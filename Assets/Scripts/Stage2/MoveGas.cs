using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGas : MonoBehaviour
{
    private bool isTriggered;

    [SerializeField]
    Rigidbody gasRigid;
    [SerializeField]
    private float moveSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if(!isTriggered)
        {
            if (other.CompareTag("Player"))
            {
                isTriggered = true;
                gasRigid.velocity = Vector3.forward * moveSpeed;
            }
        }
    }

}
