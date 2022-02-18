using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour
{
    Rigidbody squidRigid;

    private bool isPowered = false;
    private int damage = 1;
    private bool eatable = false;

    [SerializeField]
    private float SquidThrowPower;

    [SerializeField]
    private float autoDestroyTime;
    [SerializeField]
    private float eatableTime;
    private float currentEatableTime;

    //필요한 컴포넌트
    Boss bossInfo;
    PlayerController thePlayer;

    private void Start()
    {
        squidRigid = GetComponent<Rigidbody>();
        bossInfo = GameObject.FindObjectOfType<Boss>();
        thePlayer = GameObject.FindObjectOfType<PlayerController>();
        Destroy(gameObject, autoDestroyTime);
        currentEatableTime = eatableTime;
    }

    private void Update()
    {
        ThrowSquid();
        CalcEatable();
        ChangeEatable();
    }

    private void CalcEatable()
    {
        if (currentEatableTime >= 0)
        {
            currentEatableTime -= Time.deltaTime;
        }
    }

    private void ChangeEatable()
    {
        if(currentEatableTime <= 0)
        {
            eatable = true;
        }
    }

    private void ThrowSquid()
    {
        if (!isPowered)
        {
            squidRigid.AddRelativeForce(Vector3.forward * SquidThrowPower, ForceMode.Impulse);
            isPowered = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BossHitPoint"))
        {
            Debug.Log("hit");
            if (bossInfo.isGroggy)
            {
                bossInfo.DamageBoss(damage);
            }
        }
        else if(eatable && collision.gameObject.CompareTag("Player"))
        {
            eatable = false;
            thePlayer.AquireSquid();
            Destroy(gameObject);
        }

    }

    

}
