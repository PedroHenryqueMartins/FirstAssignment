using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollowPlayer : MonoBehaviour
{
    public Transform target;
    
    public Vector3 offset;

    void Update()
    {
        target = GameObject.Find("Player").transform;
        transform.position = target.position + offset;
    }
}
