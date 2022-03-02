using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearDieCount : MonoBehaviour
{
    TextMeshProUGUI text;
    private int maxScore;
    private int score;
    private int[] dieCount = new int[3];

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        maxScore = 1000;
        GetDieCount();
    }

    void Start()
    {
        CalcPlayScore();
        CalcDieScore();
        text.text = "You Died at\nStage1: " + dieCount[0].ToString() + "\nStage2: " + dieCount[1].ToString() + "\nBossStage: " + dieCount[2].ToString()
            + "\nPlaytime: " + ShowPlayTime() + "\nScore: " + score.ToString();
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

    private void CalcPlayScore()
    {
        score = maxScore - (int)GameManager.instance.playTime;
    }

    private string ShowPlayTime()
    {
        int minute;
        int sec;

        minute = (int)GameManager.instance.playTime / 60;
        sec = (int)GameManager.instance.playTime % 60;

        return minute.ToString() + ":" + sec.ToString();
    }

    private void CalcDieScore()
    {
        for (int i = 0; i < GameManager.instance.DeathCount.Length; i++)
        {
            score -= GameManager.instance.DeathCount[i]*10;
        }
        if(score < 100)
        {
            score = 100;
        }
    }

}
