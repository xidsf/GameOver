using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Boss Status")]
    [SerializeField]
    private int BossMaxHP;
    [SerializeField]
    private int currentBossHP; //ser삭제
    [SerializeField]
    private int BossATK;

    [Header("Test")]
    public bool isGroggy; //hide
    public bool isIdle; //private
    public bool isRising;
    public bool isFalling;

    [Header("Boss Move")]
    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private float risingVelocity;
    [SerializeField]
    private float FallingVelocity;
    [SerializeField]
    private float GroggyTime;
    private float currentGroggyTime;
    [SerializeField]
    private float BossRotateSpeed;

    [Header("difficulty")]
    [SerializeField]
    private float[] AttackDelay = new float[3];
    private float currentAttackDelay;
    [SerializeField]
    private int[] AttackCount = new int[3];
    private int currentAttackCount;
    [SerializeField]
    private int currentDifficulty = 1;

    [Header("Components")]
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private GameObject BossShield;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject DangerZone;
    [SerializeField]
    GameObject HitPoint;
    Rigidbody rigid;

    RaycastHit hit;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();

        currentAttackDelay = AttackDelay[currentDifficulty];
        currentBossHP = BossMaxHP;
        isRising = true;
    }

    void Update()
    {
        Rise();
        Fall();
        Shield();
        CalcGroggyTime();
        CalcAttackDelay();
        TryAttack();
        BossRotate();
        AttackCountCheck();
        ChangeDifficulty();
    }

    private void Rise()
    {
        if(isRising)
        {
            if (transform.position.y <= maxHeight)
            {
                rigid.velocity = Vector3.up * risingVelocity;
            }
            else if (transform.position.y >= maxHeight)
            {
                rigid.velocity = Vector3.zero;
                isRising = false;
                isIdle = true;
            }
        }
    }

    private void Fall()
    {
        if(isFalling)
        {
            rigid.velocity = Vector3.down * FallingVelocity;
        }
    }

    private void Shield()
    {
        if(isGroggy)
        {
            BossShield.SetActive(false);
            HitPoint.SetActive(true);
        }
        else
        {
            BossShield.SetActive(true);
            HitPoint.SetActive(false);
        }
    }

    private void CalcGroggyTime()
    {
        if(isGroggy && currentGroggyTime > 0)
        {
            currentGroggyTime -= Time.deltaTime;
        }
        else if(currentGroggyTime <= 0)
        {
            isGroggy = false;
            isRising = true;
            currentGroggyTime = GroggyTime;
        }
    }

    private void ChangeDifficulty()
    {
        if(currentBossHP == 2)
        {
            currentDifficulty = 2;
        }
        else if(currentBossHP == 1)
        {
            currentDifficulty = 3;
        }
        else
        {
            currentDifficulty = 1;
        }
    }

    private void BossRotate()
    {
        if(isIdle)
        {
            rigid.angularVelocity = Vector3.Lerp(rigid.angularVelocity, Vector3.up * BossRotateSpeed * 5, 0.01f);
        }
        else if(isIdle && currentDifficulty == 3)
        {
            rigid.angularVelocity = Vector3.Lerp(rigid.angularVelocity, -Vector3.up * BossRotateSpeed * 10, 0.02f);
        }
        else
        {
            rigid.angularVelocity = Vector3.Lerp(rigid.angularVelocity, Vector3.zero, 0.1f);
        }
    }

    public void DamageBoss(int _Dmg)
    {
        currentBossHP -= _Dmg;
        isGroggy = false;
        isRising = true;
    }

    private void CalcAttackDelay()
    {
        if(currentAttackDelay > 0)
        {
            currentAttackDelay -= Time.deltaTime;
        }
    }

    private void TryAttack()
    {
        if(isIdle && currentAttackDelay <= 0)
        {
            Attack();
            currentAttackDelay = AttackDelay[currentDifficulty - 1];
        }
    }

    private void Attack()
    {
        Physics.Raycast(new Vector3(Player.transform.position.x, 50, Player.transform.position.z), Vector3.down, out hit, 200f, layerMask);//하드코딩
        Instantiate(DangerZone, hit.point + Vector3.up * 0.1f, Quaternion.identity);
        currentAttackCount++;
    }

    private void AttackCountCheck()
    {
        if(currentAttackCount == AttackCount[currentDifficulty - 1])
        {
            isIdle = false;
            isFalling = true;
            currentAttackCount = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isFalling)
        {
            isFalling = false;
            isGroggy = true;
        }
    }

}
