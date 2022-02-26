using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearDieCount : MonoBehaviour
{
    TextMeshProUGUI text;

    private int[] dieCount = new int[3];

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        GetDieCount();
    }

    void Start()
    {
        text.text = "You Died at\nStage1: " + dieCount[0].ToString() + "\nStage2: " + dieCount[1].ToString() + "\nBossStage: " + dieCount[2].ToString();
        DeathCountReset();
    }

    private void GetDieCount()
    {
        for (int i = 1; i < dieCount.Length + 1; i++)
        {
            dieCount[i - 1] = GameManager.instance.DeathCount[i];
        }
    }

    private void DeathCountReset()
    {
        for (int i = 0; i < 4; i++)
        {
            GameManager.instance.DeathCount[i] = 0;
        }
    }

    
}
