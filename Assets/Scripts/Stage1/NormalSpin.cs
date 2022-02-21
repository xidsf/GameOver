using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSpin : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed;
    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rigid.angularVelocity = new Vector3(spinSpeed, 0, 0);
    }

}
