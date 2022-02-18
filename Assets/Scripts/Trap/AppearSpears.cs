using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearSpears : MonoBehaviour
{
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private float appearTime;
    [SerializeField]
    private float disappearTime;
    [SerializeField]
    private float disappearVelocity;
    [SerializeField]
    private float appearVelocity;

    [SerializeField]
    private Vector3 appearPosition;
    [SerializeField]
    private Vector3 disappearPosition;

    private bool BeforeStart;
    private bool isUp = true;
    private bool isWaiting;
    private bool isRunning;
    Rigidbody rigid;


    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        StartCoroutine("UpandDownCoroutine");
    }


    IEnumerator UpandDownCoroutine()
    {
        if(!BeforeStart)
        {
            BeforeStart = true;
            yield return new WaitForSeconds(startDelay);
        }
        if (!isRunning)
        {
            isRunning = true;
            if (isUp)
            {
                if (transform.localPosition.y < appearPosition.y)
                {
                    rigid.velocity = new Vector3(0, appearVelocity, 0);
                }
                else if (!isWaiting)
                {
                    isWaiting = true;
                    rigid.velocity = Vector3.zero;
                    yield return new WaitForSeconds(appearTime);
                    isWaiting = false;
                    isUp = false;
                }
            }
            else
            {
                if (transform.localPosition.y > disappearPosition.y)
                {
                    rigid.velocity = new Vector3(0, -disappearVelocity);
                }
                else if (!isWaiting)
                {
                    isWaiting = true;
                    rigid.velocity = Vector3.zero;
                    yield return new WaitForSeconds(disappearTime);
                    isWaiting = false;
                    isUp = true;
                }
            }
            isRunning = false;
        }

    }
}
