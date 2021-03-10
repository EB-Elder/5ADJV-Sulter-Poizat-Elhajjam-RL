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

    [SerializeField] private TextMesh plane00;
    [SerializeField] private TextMesh plane01;
    [SerializeField] private TextMesh plane02;
    [SerializeField] private TextMesh plane03;
    [SerializeField] private TextMesh plane04;

    [SerializeField] private TextMesh plane10;
    [SerializeField] private TextMesh plane11;
    [SerializeField] private TextMesh plane12;
    [SerializeField] private TextMesh plane13;
    [SerializeField] private TextMesh plane14;

    [SerializeField] private TextMesh plane20;
    [SerializeField] private TextMesh plane21;
    [SerializeField] private TextMesh plane22;
    [SerializeField] private TextMesh plane23;
    [SerializeField] private TextMesh plane24;

    [SerializeField] private TextMesh plane30;
    [SerializeField] private TextMesh plane31;
    [SerializeField] private TextMesh plane32;
    [SerializeField] private TextMesh plane33;
    [SerializeField] private TextMesh plane34;

    [SerializeField] private TextMesh plane40;
    [SerializeField] private TextMesh plane41;
    [SerializeField] private TextMesh plane42;
    [SerializeField] private TextMesh plane43;
    [SerializeField] private TextMesh plane44;

    public int IADecision = -1;
    
    private bool _canMove = true;

    private float _timeCount;
    private int _actualIndex;
    private float _timeMax = 0.1f;
    private bool fini = false;
    
    public void displayValueIG()
    {

        plane00.text = ia.listeEtat[0, 0].value.ToString();
        plane01.text = ia.listeEtat[0, 1].value.ToString();
        plane02.text = ia.listeEtat[0, 2].value.ToString();
        plane03.text = ia.listeEtat[0, 3].value.ToString();
        plane04.text = ia.listeEtat[0, 4].value.ToString();

        plane10.text = ia.listeEtat[1, 0].value.ToString();
        plane11.text = ia.listeEtat[1, 1].value.ToString();
        plane12.text = ia.listeEtat[1, 2].value.ToString();
        plane13.text = ia.listeEtat[1, 3].value.ToString();
        plane14.text = ia.listeEtat[1, 4].value.ToString();

        plane20.text = ia.listeEtat[2, 0].value.ToString();
        plane21.text = ia.listeEtat[2, 1].value.ToString();
        plane22.text = ia.listeEtat[2, 2].value.ToString();
        plane23.text = ia.listeEtat[2, 3].value.ToString();
        plane24.text = ia.listeEtat[2, 4].value.ToString();

        plane30.text = ia.listeEtat[3, 0].value.ToString();
        plane31.text = ia.listeEtat[3, 1].value.ToString();
        plane32.text = ia.listeEtat[3, 2].value.ToString();
        plane33.text = ia.listeEtat[3, 3].value.ToString();
        plane34.text = ia.listeEtat[3, 4].value.ToString();

        plane40.text = ia.listeEtat[4, 0].value.ToString();
        plane41.text = ia.listeEtat[4, 1].value.ToString();
        plane42.text = ia.listeEtat[4, 2].value.ToString();
        plane43.text = ia.listeEtat[4, 3].value.ToString();
        plane44.text = ia.listeEtat[4, 4].value.ToString();

    }
    
    // Start is called before the first frame update
    void Start()
    {
        player.position = startingTile.position;

        ia.Initialisation();
        ia.ValueIteration();
        displayValueIG();
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

                    if (ia.getEtatFromPos(iaPositionX, iaPositionY-1).value > valueMax)
                    {
                        valueMax = ia.getEtatFromPos(iaPositionX, iaPositionY - 1).value;
                        bestChoice = action;
                        currentState = ia.getEtatFromPos(iaPositionX, iaPositionY - 1);
                        
                    }
                break;

                case codeAction.DROITE:

                    if (ia.getEtatFromPos(iaPositionX+1, iaPositionY).value > valueMax)
                    {
                        valueMax = ia.getEtatFromPos(iaPositionX + 1, iaPositionY).value;
                        bestChoice = action;
                        currentState = ia.getEtatFromPos(iaPositionX + 1, iaPositionY);
                        
                    }
                break;

                case codeAction.GAUCHE:

                    if (ia.getEtatFromPos(iaPositionX-1, iaPositionY).value > valueMax)
                    {
                        valueMax = ia.getEtatFromPos(iaPositionX - 1, iaPositionY).value;
                        bestChoice = action;
                        currentState = ia.getEtatFromPos(iaPositionX - 1, iaPositionY);
                        
                    }
                    break;

                case codeAction.HAUT:

                    if (ia.getEtatFromPos(iaPositionX, iaPositionY + 1).value > valueMax)
                    {
                        valueMax = ia.getEtatFromPos(iaPositionX, iaPositionY + 1).value;
                        bestChoice = action;
                        currentState = ia.getEtatFromPos(iaPositionX, iaPositionY + 1);
                        
                    }
                    break;
            }
        }

        if (bestChoice == codeAction.BAS /*&& player.position.z < 0*/)
        { 
            player.Translate(0, 0, 10);
            _canMove = true;
            IADecision = -1;

            iaPositionY--;
        }

        if (bestChoice == codeAction.HAUT /*&& player.position.z > -40*/)
        {
            player.Translate(0, 0, -10);
            _canMove = true;
            IADecision = -1;


            iaPositionY++;
        }

        if (bestChoice == codeAction.DROITE /*&& player.position.x < 0*/)
        {
            player.Translate(-10, 0, 0);
            _canMove = true;
            IADecision = -1;
            iaPositionX++;
        }

        if (bestChoice == codeAction.GAUCHE /*&& player.position.x > -40*/)
        {
            player.Translate(10, 0, 0);
            _canMove = true;
            IADecision = -1;

            iaPositionX--;
        }

        if (IADecision == -1)
            _canMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {  
        if(!fini)
        {
            if (!autoPilote)
                playerController();
            else
            {
                _timeCount += Time.deltaTime;
                if (_timeCount >= _timeMax)
                {
                    _timeCount = 0.0f;
                }
                IAController();
            }

            if (player.position.x >= finishTile.position.x - 1 &&
                player.position.z >= finishTile.position.z - 1)
            {
                print("Done");
                fini = true;
                /*#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif*/
            }
        }
    }
}
