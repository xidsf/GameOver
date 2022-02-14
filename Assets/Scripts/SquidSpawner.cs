using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidSpawner : MonoBehaviour
{
    [SerializeField]
    private float SquidSpawnTime;
    [SerializeField]
    GameObject SquidPrefab;


    private void Start()
    {
        StartCoroutine("SpawnSquidCoroutine");
    }

    IEnumerator SpawnSquidCoroutine()
    {
        yield return new WaitForSeconds(SquidSpawnTime);
        Instantiate(SquidPrefab, transform.position, Quaternion.identity);
        yield return null;

    }

}
