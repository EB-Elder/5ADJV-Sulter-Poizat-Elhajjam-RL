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

    public Material textureHaut;
    public Material textureBas;
    public Material textureGauche;
    public Material textureDroite;


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


    [SerializeField] private MeshRenderer strat00;
    [SerializeField] private MeshRenderer strat01;
    [SerializeField] private MeshRenderer strat02;
    [SerializeField] private MeshRenderer strat03;
    [SerializeField] private MeshRenderer strat04;
                             
    [SerializeField] private MeshRenderer strat10;
    [SerializeField] private MeshRenderer strat11;
    [SerializeField] private MeshRenderer strat12;
    [SerializeField] private MeshRenderer strat13;
    [SerializeField] private MeshRenderer strat14;
                             
    [SerializeField] private MeshRenderer strat20;
    [SerializeField] private MeshRenderer strat21;
    [SerializeField] private MeshRenderer strat22;
    [SerializeField] private MeshRenderer strat23;
    [SerializeField] private MeshRenderer strat24;
                             
    [SerializeField] private MeshRenderer strat30;
    [SerializeField] private MeshRenderer strat31;
    [SerializeField] private MeshRenderer strat32;
    [SerializeField] private MeshRenderer strat33;
    [SerializeField] private MeshRenderer strat34;
                             
    [SerializeField] private MeshRenderer strat40;
    [SerializeField] private MeshRenderer strat41;
    [SerializeField] private MeshRenderer strat42;
    [SerializeField] private MeshRenderer strat43;
    [SerializeField] private MeshRenderer strat44;

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

    public void displayStratIG()
    {
        ia.listeEtat[0, 0].stratDisplayer = strat00;
        ia.listeEtat[0, 1].stratDisplayer = strat01;
        ia.listeEtat[0, 2].stratDisplayer = strat02;
        ia.listeEtat[0, 3].stratDisplayer = strat03;
        ia.listeEtat[0, 4].stratDisplayer = strat04;

        ia.listeEtat[1, 0].stratDisplayer = strat10;
        ia.listeEtat[1, 1].stratDisplayer = strat11;
        ia.listeEtat[1, 2].stratDisplayer = strat12;
        ia.listeEtat[1, 3].stratDisplayer = strat13;
        ia.listeEtat[1, 4].stratDisplayer = strat14;

        ia.listeEtat[2, 0].stratDisplayer = strat20;
        ia.listeEtat[2, 1].stratDisplayer = strat21;
        ia.listeEtat[2, 2].stratDisplayer = strat22;
        ia.listeEtat[2, 3].stratDisplayer = strat23;
        ia.listeEtat[2, 4].stratDisplayer = strat24;

        ia.listeEtat[3, 0].stratDisplayer = strat30;
        ia.listeEtat[3, 1].stratDisplayer = strat31;
        ia.listeEtat[3, 2].stratDisplayer = strat32;
        ia.listeEtat[3, 3].stratDisplayer = strat33;
        ia.listeEtat[3, 4].stratDisplayer = strat34;

        ia.listeEtat[4, 0].stratDisplayer = strat40;
        ia.listeEtat[4, 1].stratDisplayer = strat41;
        ia.listeEtat[4, 2].stratDisplayer = strat42;
        ia.listeEtat[4, 3].stratDisplayer = strat43;
        ia.listeEtat[4, 4].stratDisplayer = strat44;


        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {

                switch (ia.listeEtat[i,j].strategie)
                {
                    case codeAction.BAS:

                        ia.listeEtat[i, j].stratDisplayer.material = textureBas;

                        break;


                    case codeAction.HAUT:

                        ia.listeEtat[i, j].stratDisplayer.material= textureHaut;

                        break;


                    case codeAction.GAUCHE:

                        ia.listeEtat[i, j].stratDisplayer.material = textureGauche;

                        break;

                    case codeAction.DROITE:

                        ia.listeEtat[i, j].stratDisplayer.material = textureDroite;

                        break;
                }

            }
        }

            
    }

        
    // Start is called before the first frame update
    void Start()
    {
        player.position = startingTile.position;

        ia.Initialisation();



        while(!(ia.PolicyImprovement()))
        {
            ia.policyEvaluation();

        }

        //ia.ValueIteration();
        //displayValueIG();
        displayStratIG();
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

                    if (ia.getEtatFromPos(iaPositionX, iaPositionY-1).value >= valueMax)
                    {
                        if(ia.getEtatFromPos(iaPositionX, iaPositionY - 1).value == valueMax)
                        {
                            bool change = false;
                            float coin = UnityEngine.Random.Range(0.0f, 1.0f);

                            if (coin > 0.5f) change = true;

                            if (change)
                            {
                                valueMax = ia.getEtatFromPos(iaPositionX, iaPositionY - 1).value;
                                bestChoice = action;
                                currentState = ia.getEtatFromPos(iaPositionX, iaPositionY - 1);
                            }
                        }
                        else
                        {
                            valueMax = ia.getEtatFromPos(iaPositionX, iaPositionY - 1).value;
                            bestChoice = action;
                            currentState = ia.getEtatFromPos(iaPositionX, iaPositionY - 1);
                        }                        
                    }
                break;

                case codeAction.DROITE:

                    if (ia.getEtatFromPos(iaPositionX+1, iaPositionY).value >= valueMax)
                    {
                        if(ia.getEtatFromPos(iaPositionX + 1, iaPositionY).value == valueMax)
                        {
                            bool change = false;
                            float coin = UnityEngine.Random.Range(0.0f, 1.0f);

                            if (coin > 0.5f) change = true;

                            if (change)
                            {
                                valueMax = ia.getEtatFromPos(iaPositionX + 1, iaPositionY).value;
                                bestChoice = action;
                                currentState = ia.getEtatFromPos(iaPositionX + 1, iaPositionY);
                            }
                        }
                        else
                        {
                            valueMax = ia.getEtatFromPos(iaPositionX + 1, iaPositionY).value;
                            bestChoice = action;
                            currentState = ia.getEtatFromPos(iaPositionX + 1, iaPositionY);
                        }                      
                    }
                break;

                case codeAction.GAUCHE:

                    if (ia.getEtatFromPos(iaPositionX-1, iaPositionY).value >= valueMax)
                    {
                        if(ia.getEtatFromPos(iaPositionX - 1, iaPositionY).value == valueMax)
                        {
                            bool change = false;
                            float coin = UnityEngine.Random.Range(0.0f, 1.0f);

                            if (coin > 0.5f) change = true;

                            if (change)
                            {
                                valueMax = ia.getEtatFromPos(iaPositionX - 1, iaPositionY).value;
                                bestChoice = action;
                                currentState = ia.getEtatFromPos(iaPositionX - 1, iaPositionY);
                            }
                        }
                        else
                        {
                            valueMax = ia.getEtatFromPos(iaPositionX - 1, iaPositionY).value;
                            bestChoice = action;
                            currentState = ia.getEtatFromPos(iaPositionX - 1, iaPositionY);
                        }                                            
                    }
                    break;

                case codeAction.HAUT:

                    if (ia.getEtatFromPos(iaPositionX, iaPositionY + 1).value >= valueMax)
                    {
                        if(ia.getEtatFromPos(iaPositionX, iaPositionY + 1).value == valueMax)
                        {
                            bool change = false;
                            float coin = UnityEngine.Random.Range(0.0f, 1.0f);

                            if (coin > 0.5f) change = true;

                            if (change)
                            {
                                valueMax = ia.getEtatFromPos(iaPositionX, iaPositionY + 1).value;
                                bestChoice = action;
                                currentState = ia.getEtatFromPos(iaPositionX, iaPositionY + 1);
                            }
                        }
                        else
                        {
                            valueMax = ia.getEtatFromPos(iaPositionX, iaPositionY + 1).value;
                            bestChoice = action;
                            currentState = ia.getEtatFromPos(iaPositionX, iaPositionY + 1);
                        }                                           
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
    /*void Update()
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
                #endif
            }
        }
    }*/
}
