using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamaged : MonoBehaviour
{
    RawImage image;
    [SerializeField]
    PlayerStatus playerStatus;
    private int PlayerHP;


    private void Start()
    {
        image = gameObject.GetComponent<RawImage>();
        PlayerHP = playerStatus.PlayerHP;
    }

    private void Update()
    {
        Fadeout();
        FadeIn();
    }

    private void Fadeout()
    {
        Color currColor = image.color;
        currColor.a = Mathf.Lerp(image.color.a, 0, 0.03f);
        image.color = currColor;
        if(Mathf.Approximately(image.color.a, 0))
        {
            currColor.a = 0;
            image.color = currColor;
        }
    }

    private void FadeIn()
    {
        if (PlayerHP > playerStatus.PlayerHP)
        {
            Color currColor = image.color;
            currColor.a = 1;
            image.color = currColor;
            PlayerHP = playerStatus.PlayerHP;
        }
        
    }
}
