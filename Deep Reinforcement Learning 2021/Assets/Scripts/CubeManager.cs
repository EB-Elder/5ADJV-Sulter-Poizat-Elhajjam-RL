using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{

    [SerializeField] public Transform transform;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material blueMat;
    [SerializeField] private Material redMat;
    [SerializeField] private Material whiteMat;

    [HideInInspector] public bool autoPilote = false;
    [HideInInspector] public bool isRed;
    [HideInInspector] public bool isBlue;
    [HideInInspector] public bool isColored;

    public void changeToBlue()
    {
        meshRenderer.material = blueMat;
        isBlue = true;
        isColored = true;
    }

    public void changeToRed()
    {
        meshRenderer.material = redMat;
        isRed = true;
        isColored = true;
    }

    public void changeToWhite()
    {
        meshRenderer.material = whiteMat;
        isRed = false;
        isBlue = false;
        isColored = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !autoPilote && !isRed && !isBlue)
            changeToBlue();

        if (Input.GetMouseButtonDown(1) && !autoPilote && !isRed && !isBlue)
            changeToRed();
    }

}
