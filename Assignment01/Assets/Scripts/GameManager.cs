using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text pointsToText;
    [SerializeField] Text lifesToText;
    public GameObject respawn;
    int levelId = 1;
    public static GameManager gameManager = null;
    public GameObject portal;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        respawn = GameObject.Find("RespawnPosition");
    }

    public void SpawnPortal()
    {
        portal.SetActive(true);
    }

    public void UpdateScoreDisplay(int currentScore)
    {
        pointsToText.text = "Score: " + currentScore.ToString();
    }

    public void UpdateLifeDisplay(int currentLifes)
    {
        lifesToText.text = "Lifes: " + currentLifes.ToString();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(levelId);
        Player.player.gameObject.transform.position = respawn.transform.position;
    }

}
