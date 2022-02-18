using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeAppearSpear : MonoBehaviour
{
    [SerializeField]
    FakeSpace FakeSpace;

    Rigidbody rigid;


    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckFakeOn();
        
    }

    private void CheckFakeOn()
    {
        if(FakeSpace.fakeOn)
        {
            DoFake();
            Invoke("AutoDestroy", 3f);
        }
    }

    private void DoFake()
    {
        rigid.velocity = new Vector3(0, 10, 0);
    }

    private void AutoDestroy()
    {
        Destroy(gameObject);
    }
}
