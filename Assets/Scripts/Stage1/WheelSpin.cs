using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    [SerializeField]
    private float WheelSpinSpeed;
    [SerializeField]
    private float FakeSpeed;
    private float applySpeed;

    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();

        applySpeed = WheelSpinSpeed;
    }

    private void Update()
    {
        rigid.angularVelocity = new Vector3(applySpeed, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            applySpeed = FakeSpeed;
        }
    }

}
