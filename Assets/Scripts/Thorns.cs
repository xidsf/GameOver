using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    PlayerStatus thePlayerStatus;

    private void Start()
    {
        thePlayerStatus = FindObjectOfType<PlayerStatus>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            thePlayerStatus.PlayerHP -= 1;
        }
    }
}
