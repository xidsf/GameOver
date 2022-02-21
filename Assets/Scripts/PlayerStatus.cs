using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [HideInInspector]
    public int PlayerHP;
    [HideInInspector]
    public bool isLying;
    [HideInInspector]
    public bool isNnockDown;

    private void Awake()
    {
        PlayerHP = 3;
    }

}
