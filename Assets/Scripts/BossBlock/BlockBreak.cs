using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreak : MonoBehaviour
{
    MeshCollider meshCollider;
    MeshRenderer meshRenderer;
    [SerializeField]
    GameObject[] Wood = new GameObject[6];

    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Squid"))
        {
            Destroy(collision.gameObject);
            DestroyCenter();
        }
        else if(collision.gameObject.CompareTag("Rock"))
        {
            if(collision.gameObject.GetComponent<BossBullet>().isFalling)
            {
                collision.gameObject.GetComponent<BossBullet>().isFalling = false;
                DestroyCenter();
            }
        }
    }

    private void DestroyCenter()
    {
        meshRenderer.enabled = false;
        meshCollider.enabled = false;
        for (int i = 0; i < Wood.Length; i++)
        {
            Wood[i].GetComponent<MeshCollider>().enabled = true;
            Wood[i].AddComponent<Rigidbody>();
            Wood[i].GetComponent<Rigidbody>().mass = 0.2f;
        }
        Invoke("AutoDestroy", 1f);
    }

    private void AutoDestroy()
    {
        Destroy(gameObject);
    }

}
