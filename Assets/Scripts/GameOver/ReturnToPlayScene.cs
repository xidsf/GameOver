using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ReturnToPlayScene : MonoBehaviour
{
    public void ReturnScene()
    {
        if(GameManager.instance.currentStage == 1)
        {
            SceneManager.LoadScene("Stage1");
        }
        else if(GameManager.instance.currentStage == 2)
        {
            SceneManager.LoadScene("Stage2");
        }
        else if(GameManager.instance.currentStage == 3)
        {
            SceneManager.LoadScene("BossStage");
        }
        else if(GameManager.instance.currentStage == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
