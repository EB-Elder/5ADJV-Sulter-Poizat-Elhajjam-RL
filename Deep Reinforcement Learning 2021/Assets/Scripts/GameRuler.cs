using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuler : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Transform finishTile;
    [SerializeField] private Transform startingTile;
    [SerializeField] private bool autoPilote;
    
    public int IADecision = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        player.position = startingTile.position;
    }

    void playerController()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && player.position.z < 0)
            player.Translate(0, 0, -10);
        if(Input.GetKeyDown(KeyCode.DownArrow) && player.position.z > -40)
            player.Translate(0, 0, 10);
        if(Input.GetKeyDown(KeyCode.RightArrow) && player.position.x < 0)
            player.Translate(-10, 0, 0);
        if(Input.GetKeyDown(KeyCode.LeftArrow) && player.position.x > -40)
            player.Translate(10, 0, 0);
    }

    void IAController()
    {
        
        if( IADecision == 1 && player.position.z < 0)
            player.Translate(0, 0, -10);
        if( IADecision == 2 && player.position.z > -40)
            player.Translate(0, 0, 10);
        if( IADecision == 3 && player.position.x < 0)
            player.Translate(-10, 0, 0);
        if( IADecision == 4 && player.position.x > -40)
            player.Translate(10, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!autoPilote)
            playerController();
        else
            IAController();
        
        if (player.position.x >= finishTile.position.x-1 && 
            player.position.z >= finishTile.position.z-1)
        {
            print("Done");

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
