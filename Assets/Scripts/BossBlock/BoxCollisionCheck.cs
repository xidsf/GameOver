using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollisionCheck : MonoBehaviour
{
    [SerializeField]
    Collider theCollider;
    public bool isbreak;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Squid"))
        {
            Destroy(collision.gameObject);
            isbreak = true;
        }
    }
}
