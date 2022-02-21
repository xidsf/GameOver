using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ReturnToPlayScene : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ReturnScene()
    {
        if(gameManager.currentStage == 1)
        {
            SceneManager.LoadScene("Stage1");
        }
        else if(gameManager.currentStage == 3)
        {
            SceneManager.LoadScene("BossStage");
        }
        else if(gameManager.currentStage == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
