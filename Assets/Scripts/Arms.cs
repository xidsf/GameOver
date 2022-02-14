using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    private void OnEnable()
    {
        anim.SetTrigger("Shoot");
        Invoke("Deactivate", 1.2f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
