using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material blueMat;
    [SerializeField] private Material redMat;
    [SerializeField] private Material whiteMat;

    [HideInInspector] public bool autoPilote = false;
    [HideInInspector] public bool isRed;
    [HideInInspector] public bool isBlue;
    [HideInInspector] public bool isColored;


    public void playerOnePlaying(bool isSimulated = false)
    {
        if(!isSimulated)
            changeColorToBlue();
        isBlue = true;
        isColored = true;
    }
    
    public void playerTwoPlaying(bool isSimulated = false)
    {
        if (!isSimulated)
            changeColorToRed();
        isRed = true;
        isColored = true;
        
    }
    
    
    public void changeColorToBlue()
    {
        meshRenderer.material = blueMat;
        
    }

    public void changeColorToRed()
    {
        meshRenderer.material = redMat;
    }

    public void changeColorToWhite()
    {
        meshRenderer.material = whiteMat;
        isRed = false;
        isBlue = false;
        isColored = false;
    }

    /*private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !autoPilote && !isRed && !isBlue)
            playerOnePlaying();

        if (Input.GetMouseButtonDown(1) && !autoPilote && !isRed && !isBlue)
            playerTwoPlaying();
    }*/

}
