using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    PlayerStatus player;
    Rigidbody rockRigid;

    private bool isFalling = true;

    private void Start()
    {
        player = FindObjectOfType<PlayerStatus>();
        rockRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(rockRigid.velocity.y <= -10)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isFalling && collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerStatus>().PlayerHP -= 1;
        }
        
    }
}
