using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    [SerializeField]
    Transform PlayerTransform;

    [SerializeField]
    GameObject[] EyeLight;
    [SerializeField]
    GameObject[] Razer;

    [SerializeField]
    private float AttackDelay;
    private bool isAttacking;

    [SerializeField]
    FinishTrigger trigger;

    private float[] attackerPos = new float[3];

    private void Start()
    {
        attackerPos[0] = -8f;
        attackerPos[1] = 0f;
        attackerPos[2] = 8f;
    }

    private void Update()
    {
        if(trigger.isTriggered)
        {
            CheckDistance();
        }
    }

    private void CheckDistance()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            int minIndex = 0;
            float minDistance = 10f;
            float tempDistance;
            for (int i = 0; i < attackerPos.Length; i++)
            {
                tempDistance = Mathf.Abs(PlayerTransform.position.x - attackerPos[i]);
                if (minDistance > tempDistance)
                {
                    minIndex = i;
                    minDistance = Mathf.Abs(PlayerTransform.position.x - attackerPos[i]);
                }
            }
            StartCoroutine(AttackRazerCoroutine(minIndex));
        }
    }

    IEnumerator AttackRazerCoroutine(int _index)
    {
        EyeLightUp(_index);
        yield return new WaitForSeconds(1.0f);
        TurnOffEyeLight(_index);
        Shoot(_index);
        yield return new WaitForSeconds(AttackDelay);
        isAttacking = false;
    }

    private void EyeLightUp(int _index)
    {
        EyeLight[_index].SetActive(true);
    }

    private void TurnOffEyeLight(int _index)
    {
        EyeLight[_index].SetActive(false);
    }

    private void Shoot(int _index)
    {
        Razer[_index].SetActive(true);
    }

}
