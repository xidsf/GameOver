using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    PlayerStatus player;
    Rigidbody rockRigid;

    [SerializeField]
    private float KnockDownTime;
    [SerializeField]
    private float DestryoTime;

    private bool isFalling = true;

    private void Start()
    {
        player = FindObjectOfType<PlayerStatus>();
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
            player.GetComponent<PlayerStatus>().PlayerHP -= 1;
            StartCoroutine("PlayerKnockDownCoroutine");
        }
        
    }

    IEnumerator PlayerKnockDownCoroutine()
    {
        player.isNnockDown = true;
        yield return new WaitForSeconds(KnockDownTime);
        player.isNnockDown = false;
        yield return null;
    }

}
