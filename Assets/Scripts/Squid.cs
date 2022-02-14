using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour
{
    Rigidbody squidRigid;

    private bool isPowered = false;
    private int Power = 1;

    //필요한 컴포넌트
    Boss bossInfo;


    private void Start()
    {
        squidRigid = GetComponent<Rigidbody>();
        bossInfo = GameObject.FindObjectOfType<Boss>();

    }

    private void Update()
    {
        if(!isPowered)
        {
            squidRigid.AddRelativeForce(Vector3.forward * 30f, ForceMode.Impulse);
            isPowered = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BossHitPoint"))
        {
            Debug.Log("tagCheck");
            if (bossInfo.isGroggy)
            {
                Debug.Log("Hit");
                bossInfo.DamageBoss(Power);
            }
        }
    }

    

}
