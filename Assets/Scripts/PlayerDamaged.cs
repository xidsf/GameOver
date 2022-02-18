using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamaged : MonoBehaviour
{
    RawImage image;
    [SerializeField]
    PlayerStatus platerStatus;
    private int PlayerHP;


    private void Awake()
    {
        image = gameObject.GetComponent<RawImage>();
        PlayerHP = platerStatus.PlayerHP;
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
        if(PlayerHP > platerStatus.PlayerHP)
        {
            Color currColor = image.color;
            currColor.a = 1;
            image.color = currColor;
            PlayerHP = platerStatus.PlayerHP;
        }
        
    }
}
