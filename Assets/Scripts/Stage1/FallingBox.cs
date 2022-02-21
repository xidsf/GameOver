using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBox : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField]
    private float DownSpeed;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Squid"))
        {
            rigid.useGravity = true;
            rigid.velocity += new Vector3(0, -DownSpeed, 0);
        }
    }
}
