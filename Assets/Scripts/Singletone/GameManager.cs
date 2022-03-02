using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [HideInInspector]
    public int[] SquidCount = new int[4];

    [HideInInspector]
    public int[] DeathCount = new int[4];

    //[HideInInspector]
    public int currentStage;
    public float playTime;

    
    private void Awake()
    {
        #region singletone
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        #endregion 

        SquidCount[0] = -1;
        SquidCount[1] = 2;
        SquidCount[2] = 1;
        SquidCount[3] = 5;

        for (int i = 0; i < DeathCount.Length; i++)
        {
            DeathCount[i] = 0;
        }

    }
}
