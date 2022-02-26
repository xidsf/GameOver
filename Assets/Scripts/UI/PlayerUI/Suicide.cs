using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour
{
    [SerializeField]
    PlayerController thePlayerController;
    [SerializeField]
    PlayerStatus thePlayerStatus;

    public void SuicidePlayer()
    {
        thePlayerStatus.PlayerHP = 1;
        thePlayerController.PlayerDamaged();
    }
}
