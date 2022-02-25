using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float disappearTime = 0.05f;
    private Vector3 originSize = new Vector3(1f, -18, 1f);
    private Vector3 thinSize = new Vector3(0, -18, 0);

    private void OnEnable()
    {
        gameObject.transform.localScale = originSize;
    }

    private void Update()
    {
        SizeDown();
    }

    private void SizeDown()
    {
        gameObject.transform.localScale = Vector3.Lerp(transform.localScale, thinSize, disappearTime);
        if (transform.localScale.x - 0.1f <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
