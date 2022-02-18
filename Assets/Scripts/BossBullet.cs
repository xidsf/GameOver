using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    PlayerStatus playerStatus;
    Rigidbody rockRigid;

    [SerializeField]
    private float KnockDownTime;
    [SerializeField]
    private float DestryoTime;

    private bool isFalling = true;

    private void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        rockRigid = GetComponent<Rigidbody>();

        Destroy(gameObject, DestryoTime);
    }

    private void Update()
    {
        VelocityCheck();
    }

    private void VelocityCheck()
    {
        if (rockRigid.velocity.y <= -10)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isFalling && collision.gameObject.CompareTag("Player"))
        {
            isFalling = false;
            playerController.PlayerDamaged();
            StartCoroutine("PlayerKnockDownCoroutine");
        }
        
    }

    IEnumerator PlayerKnockDownCoroutine()
    {
        playerStatus.isNnockDown = true;
        yield return new WaitForSeconds(KnockDownTime);
        playerStatus.isNnockDown = false;
        yield return null;
    }

}
