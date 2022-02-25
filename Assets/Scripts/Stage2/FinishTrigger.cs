using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if(!isTriggered)
        {
            if (other.CompareTag("Player"))
            {
                isTriggered = true;
            }
        }
    }
}
