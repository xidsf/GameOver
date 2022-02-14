using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private int BossHP;
    [SerializeField]
    private int BossATK;

    [SerializeField]
    private float rockSpawnDelay;
    private float currentRockSpawnDelay;

    [HideInInspector]
    public bool isGroggy;
    private bool isAttaking = true;
    private bool isIdle;

    RaycastHit hit;
    [SerializeField]
    private LayerMask layerMask;

    //필요한 컴포넌트
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject DangerZone;

    private void Start()
    {
        currentRockSpawnDelay = rockSpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        CalcRockSpawnDelay();
        TryAttack();
    }

    public void DamageBoss(int _Dmg)
    {
        BossHP -= _Dmg;
        if(_Dmg >= 1)
        {
            isGroggy = false;
        }
    }

    private void CalcRockSpawnDelay()
    {
        if(rockSpawnDelay > 0)
        {
            currentRockSpawnDelay -= Time.deltaTime;
        }
    }

    private void TryAttack()
    {
        if(isAttaking && currentRockSpawnDelay <= 0)
        {
            Attack();
            currentRockSpawnDelay = rockSpawnDelay;
        }
    }

    private void Attack()
    {
        Physics.Raycast(new Vector3(Player.transform.position.x, 50, Player.transform.position.z), Vector3.down, out hit, 200f, layerMask);//하드코딩
        Instantiate(DangerZone, hit.point + Vector3.up * 0.1f, Quaternion.identity);
    }
}
