using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDownDamageArea : MonoBehaviour
{
    PlayerController thePlayerController;
    PlayerStatus thePlayerStatus;

    [SerializeField]
    private float knockDownTime;

    void Start()
    {
        thePlayerController = FindObjectOfType<PlayerController>();
        thePlayerStatus = FindObjectOfType<PlayerStatus>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            thePlayerController.PlayerDamaged();
            StartCoroutine("PlayerKnockDownCoroutine");
        }
    }

    IEnumerator PlayerKnockDownCoroutine()
    {
        if (thePlayerStatus.PlayerHP > 0 && thePlayerController.isInvincible)
        {
            thePlayerStatus.isNnockDown = true;
            yield return new WaitForSeconds(knockDownTime);
            thePlayerStatus.isNnockDown = false;
            yield return null;
        }
    }


}
