using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunSword : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }
}
