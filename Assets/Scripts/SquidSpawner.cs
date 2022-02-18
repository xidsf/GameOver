using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidSpawner : MonoBehaviour
{
    [SerializeField]
    private float SquidSpawnTime;
    private float currentSquidSpawnTime;
    [SerializeField]
    GameObject SquidPrefab;


    private void Start()
    {
        currentSquidSpawnTime = SquidSpawnTime;
    }

    private void Update()
    {
        CalcSpawnTime();
        SpawnSquid();
    }

    private void CalcSpawnTime()
    {
        if(currentSquidSpawnTime > 0)
        {
            currentSquidSpawnTime -= Time.deltaTime;
        }
    }

    private void SpawnSquid()
    {
        if(currentSquidSpawnTime <= 0)
        {
            GameObject clone = Instantiate(SquidPrefab, transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 30f, ForceMode.Impulse);
            currentSquidSpawnTime = SquidSpawnTime;

        }
    }


}
