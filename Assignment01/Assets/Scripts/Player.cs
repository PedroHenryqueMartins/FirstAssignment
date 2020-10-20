using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public enum Jumps
    {
        Grounded,
        Jump,
        DoubleJump,
    }

    public float moveSpeed = 5.0f;
    public float jumpHeight = 100.0f;
    
    public int score = 0;
    public int lifes = 3;
    public static Player player = null;
    Rigidbody rb;
    public GameObject gameController;
    Jumps _jumpState = Jumps.Grounded;

    void Awake()
    {
        GameManager gm = gameController.GetComponent<GameManager>();
        ServiceLocator.Register<GameManager>(gm);
        if (player == null)
        {
            player = this;
        }
        else if (player != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Jump();
        if (score >= 4)
        {
            ServiceLocator.Get<GameManager>().SpawnPortal();
        }

        if (score >= 7)
        {
            Debug.Log("YOU WIN!");
        }

    }

    void FixedUpdate()
    {
        Movement();
        gameController = GameObject.Find("GameManager");
    }

    void Movement()
    {
        float hAxis = Input.GetAxis("Horizontal");
        

        Vector3 move = new Vector3(hAxis * moveSpeed, 0.0f, 0.0f);
        rb.AddForce(move);
    }

    void Jump()
    {
        float jumpValue = Input.GetButtonDown("Jump") ? jumpHeight : 0.0f;

        switch (_jumpState)
        {
            case Jumps.Grounded:
                if (jumpValue > 0.0f)
                {
                    _jumpState = Jumps.Jump;
                }
                break;
            case Jumps.Jump:
                if (jumpValue > 0.0f)
                {
                    _jumpState = Jumps.DoubleJump;
                }
                break;
            case Jumps.DoubleJump:
                jumpValue = 0.0f;
                break;
        }

        Vector3 jumping = new Vector3(0.0f, jumpValue, 0.0f);
        rb.AddForce(jumping);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((_jumpState == Jumps.Jump || _jumpState == Jumps.DoubleJump) && collision.gameObject.CompareTag("Ground"))
        {
            _jumpState = Jumps.Grounded;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            PickUp pickup = other.gameObject.GetComponent<PickUp>();
            if (pickup != null)
            {
                score += pickup.Collect();
                ServiceLocator.Get<GameManager>().UpdateScoreDisplay(score);
            }
        }

        if (other.gameObject.CompareTag("Death"))
        {
            if (lifes <= 0)
            {
                Debug.Log("YOU LOSE!");
            }

            gameObject.transform.position = gameController.GetComponent<GameManager>().respawn.transform.position;
            lifes--;
            ServiceLocator.Get<GameManager>().UpdateLifeDisplay(lifes);
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            ServiceLocator.Get<GameManager>().NextLevel();
        }
    }
}
