using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = System.Random;


public class GameRulerTicTacToe : MonoBehaviour
{
    [SerializeField] private bool autoPilote = false;

    private Random generator = new Random();

    private bool isPlayerOne = true;
    private float _timeCount;
    private float _timeCountPlayer;
    private float _endTime = 1.0f;

    private int scoreRouge = 0;
    private int scoreBleu = 0;

    [SerializeField] private MCTS IAMCTS;
    
    [SerializeField] private List<int> _choicesLeft = new List<int>(8);
    [SerializeField] private List<CubeManager> actualBoard = new List<CubeManager>(8);

    // Start is called before the first frame update
    void Start()
    {
        IAMCTS.initMCTS(_choicesLeft, actualBoard);
        initGame();
    }

    void initGame()
    {
        _choicesLeft.Clear();
        for (int i = 0; i < actualBoard.Count; i++)
        {
            _choicesLeft.Add(i);
            actualBoard[i].autoPilote = autoPilote;
            actualBoard[i].changeColorToWhite();
        }
    }

    void IAController()
    {
        _timeCountPlayer += Time.deltaTime;
        int choice = generator.Next(_choicesLeft.Count);
        if(_timeCountPlayer >= _endTime)
        {
            if (isPlayerOne)
            {
                actualBoard[_choicesLeft[choice]].playerOnePlaying();
                _choicesLeft.RemoveAt(choice);
                isPlayerOne = false;
            }
            else
            {
                actualBoard[_choicesLeft[choice]].playerTwoPlaying();
                _choicesLeft.RemoveAt(choice);
                isPlayerOne = true;
            }

            _timeCountPlayer = 0.0f;
        }
            
        
    }

    void EndCondition()
    {
        
        if (actualBoard[0].isRed && actualBoard[1].isRed && actualBoard[2].isRed ||
            actualBoard[3].isRed && actualBoard[4].isRed && actualBoard[5].isRed ||
            actualBoard[6].isRed && actualBoard[7].isRed && actualBoard[8].isRed ||
            actualBoard[0].isRed && actualBoard[4].isRed && actualBoard[8].isRed ||
            actualBoard[6].isRed && actualBoard[4].isRed && actualBoard[2].isRed ||
            actualBoard[0].isRed && actualBoard[3].isRed && actualBoard[6].isRed ||
            actualBoard[1].isRed && actualBoard[4].isRed && actualBoard[7].isRed ||
            actualBoard[2].isRed && actualBoard[5].isRed && actualBoard[8].isRed )
        {
            _timeCount += Time.deltaTime;
            if (_timeCount >= _endTime)
            {
                initGame();
                _timeCount = 0.0f;
                scoreRouge++;
                IAMCTS.isGameOver = true;
                print($"Bleu : {scoreBleu} - Rouge : {scoreRouge}");
            }

            
        }
        
        if (actualBoard[0].isBlue && actualBoard[1].isBlue && actualBoard[2].isBlue ||
            actualBoard[3].isBlue && actualBoard[4].isBlue && actualBoard[5].isBlue ||
            actualBoard[6].isBlue && actualBoard[7].isBlue && actualBoard[8].isBlue ||
            actualBoard[0].isBlue && actualBoard[4].isBlue && actualBoard[8].isBlue ||
            actualBoard[6].isBlue && actualBoard[4].isBlue && actualBoard[2].isBlue ||
            actualBoard[0].isBlue && actualBoard[3].isBlue && actualBoard[6].isBlue ||
            actualBoard[1].isBlue && actualBoard[4].isBlue && actualBoard[7].isBlue ||
            actualBoard[2].isBlue && actualBoard[5].isBlue && actualBoard[8].isBlue )
        {
            _timeCount += Time.deltaTime;
            if (_timeCount >= _endTime)
            {
                initGame();
                _timeCount = 0.0f;
                scoreBleu++;
                IAMCTS.isGameOver = true;
                print($"Bleu : {scoreBleu} - Rouge : {scoreRouge}");
            }

            
        }

        if (actualBoard[0].isColored &&
            actualBoard[1].isColored &&
            actualBoard[2].isColored &&
            actualBoard[3].isColored &&
            actualBoard[4].isColored &&
            actualBoard[5].isColored &&
            actualBoard[6].isColored &&
            actualBoard[7].isColored &&
            actualBoard[8].isColored)
        {
            _timeCount += Time.deltaTime;
            if (_timeCount >= _endTime)
            {
                
                print("Égalité !");
                initGame();
                _timeCount = 0.0f;
                IAMCTS.isGameOver = true;
                print($"Bleu : {scoreBleu} - Rouge : {scoreRouge}");
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < actualBoard.Count; i++)
        {
            actualBoard[i].autoPilote = autoPilote;
        }

        if (autoPilote)
        {
            //IAController();
        }
        
        EndCondition();
        
        

        

    }
}
