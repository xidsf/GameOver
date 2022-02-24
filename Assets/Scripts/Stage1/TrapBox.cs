using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBox : MonoBehaviour
{
    [SerializeField]
    BoxMove boxMove;
    
    private bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if(!isTriggered)
        {
            if (other.CompareTag("Player") || other.CompareTag("Squid"))
            {
                isTriggered = true;
                boxMove.StartMoveCoroutine();
            }
        }
    }

}
