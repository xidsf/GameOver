using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSpace : MonoBehaviour
{
    [HideInInspector]
    public bool fakeOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!fakeOn)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Squid"))
            {
                fakeOn = true;
            }
        }
    }
}
