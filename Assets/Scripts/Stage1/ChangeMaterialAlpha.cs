using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialAlpha : MonoBehaviour
{
    [SerializeField]
    Material RedFieldMaterial;

    Color color;
    [SerializeField]
    private float alpha1;
    [SerializeField]
    private float alpha2;

    private float currentAlpha;
    private bool isBright;

    private void Start()
    {
        color = RedFieldMaterial.color;
        color.a = alpha1;
        RedFieldMaterial.color = color;

        currentAlpha = alpha1;

    }

    
    void Update()
    {
        SetColorAlpha();
        
    }

    private void SetColorAlpha()
    {
        color = RedFieldMaterial.color;
        color.a = Mathf.Lerp(color.a, currentAlpha, 0.02f);
        RedFieldMaterial.color = color;

        if(isBright)
        {
            if (color.a - 0.1 <= currentAlpha)
            {
                isBright = !isBright;
                ChangeCurrentAlpha();
            }
        }
        else
        {
            if (color.a + 0.1 >= currentAlpha)
            {
                isBright = !isBright;
                ChangeCurrentAlpha();
            }
        }
    }

    private void ChangeCurrentAlpha()
    {
        if (isBright)
        {
            currentAlpha = alpha1;
        }
        else
        {
            currentAlpha = alpha2;
        }
    }

}
