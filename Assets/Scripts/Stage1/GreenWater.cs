using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWater : MonoBehaviour
{
    PlayerController thePlayer;

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            thePlayer.PlayerDamaged();
        }
    }
}
