using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChangeToBoss : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    private void Awake()
    {
        gameManager.currentStage = 3;
    }
}
