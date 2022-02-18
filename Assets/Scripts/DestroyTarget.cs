using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    [SerializeField]
    AudioClip BreakSE;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BreakSE;
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
    }
}
