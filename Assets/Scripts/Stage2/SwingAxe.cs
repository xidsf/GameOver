using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAxe : MonoBehaviour
{
    [SerializeField]
    private float angle = 0;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool isSin;

    private float lerpTime = 0;

    private void Update()
    {
        lerpTime += Time.deltaTime * speed;
        transform.rotation = CalculateMovementOfPendulum();
    }

    private Quaternion CalculateMovementOfPendulum()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle), Quaternion.Euler(Vector3.back * angle), GetLerpTParam());
    }

    private float GetLerpTParam()
    {
        if(isSin)
        {
            return (Mathf.Sin(lerpTime) + 1) * 0.5f;
        }
        else
        {
            return (Mathf.Cos(lerpTime) + 1) * 0.5f;
        }
        
    }
}
