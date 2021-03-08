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
    
    private bool _canMove = true;

    private float _timeCount;
    private int _actualIndex;
    private float _timeMax = 0.1f;
    
    private List<int> _decisionArray = new List<int>()
    {
        1, 4, 4, 1, 1, 4, 2, 2, 4, 1, 2, 4, 2, 1, 3, 3, 4, 3, 2, 4, 1, 4, 2, 3, 1, 2, 2, 4, 1,
        4, 4, 4, 1, 1, 1, 2, 2, 1, 1, 3, 4, 4, 4, 1, 2, 2, 3, 4, 1, 4, 1, 2, 1, 2, 3, 4, 2, 3,
        4, 4, 4, 2, 4, 2, 1, 1, 2, 3, 3, 1, 4, 1, 3, 2, 4, 3, 2, 3, 1, 1, 3, 1, 4, 3, 3, 1, 2,
        2, 3, 3, 2, 3, 3, 4, 3, 2, 3, 1, 1, 1
    }; 
    
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

        if (IADecision == 1 && player.position.z < 0)
        { 
            player.Translate(0, 0, -10);
            _canMove = false;
            IADecision = 0;
        }

        if (IADecision == 2 && player.position.z > -40)
        {
            player.Translate(0, 0, 10);
            _canMove = false;
            IADecision = 0;
        }

        if (IADecision == 3 && player.position.x < 0)
        {
            player.Translate(-10, 0, 0);
            _canMove = false;
            IADecision = 0;
        }

        if (IADecision == 4 && player.position.x > -40)
        {
            player.Translate(10, 0, 0);
            _canMove = false;
            IADecision = 0;
        }

        if (IADecision == 0)
            _canMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {

        
        
        
        
        if(!autoPilote)
            playerController();
        else
        {
            _timeCount += Time.deltaTime;
            if (_timeCount >= _timeMax)
            {
                if (_actualIndex >= _decisionArray.Count)
                    _actualIndex = 0;
            
                IADecision = _decisionArray[_actualIndex];
            
                _actualIndex++;
                _timeCount = 0.0f;
            }
            IAController();
        }
        
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
