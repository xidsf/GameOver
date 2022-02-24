using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    PlayerController thePlayerController;

    private void Start()
    {
        thePlayerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            thePlayerController.PlayerDamaged();
        }
    }
}
