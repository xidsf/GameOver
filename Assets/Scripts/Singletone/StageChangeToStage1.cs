using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChangeToStage1 : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    private void Awake()
    {
        gameManager.currentStage = 1;
    }
}
