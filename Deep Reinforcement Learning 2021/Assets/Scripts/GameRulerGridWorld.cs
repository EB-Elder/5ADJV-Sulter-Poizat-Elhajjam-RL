using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRulerGridWorld : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Transform finishTile;
    [SerializeField] private Transform startingTile;
    [SerializeField] private bool autoPilote;
    [SerializeField] ValueIterationScript ia;
    [SerializeField] private int iaPositionX;
    [SerializeField] private int iaPositionY;

    public int IADecision = -1;
    
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

        ia.Initialisation();

        ia.ValueIteration();

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
        Etat currentState = ia.getEtatFromPos(iaPositionX, iaPositionY);
        List<codeAction> actionPossible = ia.getPossibleActions(currentState);

        float valueMax = -1000000;

        codeAction bestChoice = codeAction.HAUT;

        foreach (codeAction action in actionPossible)
        {
            switch (action)
            {
                case codeAction.BAS:

                    if (ia.getEtatFromPos(iaPositionX, iaPositionY+1).value > valueMax)
                    {
                        valueMax = ia.getEtatFromPos(iaPositionX, iaPositionY + 1).value;
                        bestChoice = action;
                        currentState = ia.getEtatFromPos(iaPositionX, iaPositionY + 1);
                        iaPositionY++;
                    }
                break;

                case codeAction.DROITE:

                    if (ia.getEtatFromPos(iaPositionX+1, iaPositionY).value > valueMax)
                    {
                        valueMax = ia.getEtatFromPos(iaPositionX + 1, iaPositionY).value;
                        bestChoice = action;
                        currentState = ia.getEtatFromPos(iaPositionX + 1, iaPositionY);
                        iaPositionX++;
                    }
                break;

                case codeAction.GAUCHE:

                    if (ia.getEtatFromPos(iaPositionX-1, iaPositionY).value > valueMax)
                    {
                        valueMax = ia.getEtatFromPos(iaPositionX - 1, iaPositionY).value;
                        bestChoice = action;
                        currentState = ia.getEtatFromPos(iaPositionX - 1, iaPositionY);
                        iaPositionX--;
                    }
                    break;

                case codeAction.HAUT:

                    if (ia.getEtatFromPos(iaPositionX, iaPositionY - 1).value > valueMax)
                    {
                        valueMax = ia.getEtatFromPos(iaPositionX, iaPositionY - 1).value;
                        bestChoice = action;
                        currentState = ia.getEtatFromPos(iaPositionX, iaPositionY - 1);
                        iaPositionY--;
                    }
                    break;
            }
        }





        if (bestChoice == codeAction.BAS && player.position.z < 0)
        { 
            player.Translate(0, 0, 10);
            _canMove = true;
            IADecision = -1;
            Debug.Log("on va en bas");
        }

        if (bestChoice == codeAction.HAUT && player.position.z > -40)
        {
            player.Translate(0, 0, -10);
            _canMove = true;
            IADecision = -1;

            Debug.Log("on va en haut");
        }

        if (bestChoice == codeAction.DROITE && player.position.x < 0)
        {
            player.Translate(-10, 0, 0);
            _canMove = true;
            IADecision = -1;
            Debug.Log("on va à droite");
        }

        if (bestChoice == codeAction.GAUCHE && player.position.x > -40)
        {
            player.Translate(10, 0, 0);
            _canMove = true;
            IADecision = -1;

            Debug.Log("on va à gauche");
        }

        if (IADecision == -1)
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
