using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }


    public void StartMoveCoroutine()
    {
        StartCoroutine("TrapActivateCoroutine");
    }
    
    IEnumerator TrapActivateCoroutine()
    {
        rigid.velocity = Vector3.right * 30;
        yield return new WaitForSeconds(0.4f);
        rigid.velocity = Vector3.zero;
    }
}
