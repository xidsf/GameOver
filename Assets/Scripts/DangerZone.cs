using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField]
    GameObject Rock;

    [SerializeField]
    private float height = 30f;
    private Vector3 SpawnRockPosition;
    public float SpawnDelay = 1f;

    private void Start()
    {
        SpawnRockPosition = transform.position + Vector3.up * height;
        StartCoroutine("ShowDangerZone");
    }

    IEnumerator ShowDangerZone()
    {
        yield return new WaitForSeconds(SpawnDelay);
        Instantiate(Rock, SpawnRockPosition, Quaternion.identity);
        Destroy(gameObject);
    }

}
