using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = System.Random;

public class GameRulerTicTacToe : MonoBehaviour
{
    
    [SerializeField] private CubeManager[] cubeManagers;
    [SerializeField] private bool autoPilote = false;

    private Random generator = new Random();

    private bool isPlayerOne = true;
    private float _timeCount;
    private float _timeCountPlayer;
    private float _endTime = 1.0f;

    private int scoreRouge = 0;
    private int scoreBleu = 0;
    
    [SerializeField] private List<int> _choicesLeft = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        initGame();
    }

    void initGame()
    {
        _choicesLeft.Clear();
        for (int i = 0; i < cubeManagers.Length; i++)
        {
            _choicesLeft.Add(i);
            cubeManagers[i].autoPilote = autoPilote;
            cubeManagers[i].changeToWhite();
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
                cubeManagers[_choicesLeft[choice]].changeToBlue();
                _choicesLeft.RemoveAt(choice);
                isPlayerOne = false;
            }
            else
            {
                cubeManagers[_choicesLeft[choice]].changeToRed();
                _choicesLeft.RemoveAt(choice);
                isPlayerOne = true;
            }

            _timeCountPlayer = 0.0f;
        }
            
        
    }

    void EndCondition()
    {
        
        if (cubeManagers[0].isRed && cubeManagers[1].isRed && cubeManagers[2].isRed ||
            cubeManagers[3].isRed && cubeManagers[4].isRed && cubeManagers[5].isRed ||
            cubeManagers[6].isRed && cubeManagers[7].isRed && cubeManagers[8].isRed ||
            cubeManagers[0].isRed && cubeManagers[4].isRed && cubeManagers[8].isRed ||
            cubeManagers[6].isRed && cubeManagers[4].isRed && cubeManagers[2].isRed ||
            cubeManagers[0].isRed && cubeManagers[3].isRed && cubeManagers[6].isRed ||
            cubeManagers[1].isRed && cubeManagers[4].isRed && cubeManagers[7].isRed ||
            cubeManagers[2].isRed && cubeManagers[5].isRed && cubeManagers[8].isRed )
        {
            _timeCount += Time.deltaTime;
            if (_timeCount >= _endTime)
            {
                initGame();
                _timeCount = 0.0f;
                scoreRouge++;
                print($"Bleu : {scoreBleu} - Rouge : {scoreRouge}");
            }

            
        }
        
        if (cubeManagers[0].isBlue && cubeManagers[1].isBlue && cubeManagers[2].isBlue ||
            cubeManagers[3].isBlue && cubeManagers[4].isBlue && cubeManagers[5].isBlue ||
            cubeManagers[6].isBlue && cubeManagers[7].isBlue && cubeManagers[8].isBlue ||
            cubeManagers[0].isBlue && cubeManagers[4].isBlue && cubeManagers[8].isBlue ||
            cubeManagers[6].isBlue && cubeManagers[4].isBlue && cubeManagers[2].isBlue ||
            cubeManagers[0].isBlue && cubeManagers[3].isBlue && cubeManagers[6].isBlue ||
            cubeManagers[1].isBlue && cubeManagers[4].isBlue && cubeManagers[7].isBlue ||
            cubeManagers[2].isBlue && cubeManagers[5].isBlue && cubeManagers[8].isBlue )
        {
            _timeCount += Time.deltaTime;
            if (_timeCount >= _endTime)
            {
                initGame();
                _timeCount = 0.0f;
                scoreBleu++;
                print($"Bleu : {scoreBleu} - Rouge : {scoreRouge}");
            }

            
        }

        if (cubeManagers[0].isColored &&
            cubeManagers[1].isColored &&
            cubeManagers[2].isColored &&
            cubeManagers[3].isColored &&
            cubeManagers[4].isColored &&
            cubeManagers[5].isColored &&
            cubeManagers[6].isColored &&
            cubeManagers[7].isColored &&
            cubeManagers[8].isColored)
        {
            _timeCount += Time.deltaTime;
            if (_timeCount >= _endTime)
            {
                
                print("Égalité !");
                initGame();
                _timeCount = 0.0f;
                print($"Bleu : {scoreBleu} - Rouge : {scoreRouge}");
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cubeManagers.Length; i++)
        {
            cubeManagers[i].autoPilote = autoPilote;
        }

        if (autoPilote)
        {
            IAController();
        }
        
        EndCondition();
        
        

        

    }
}
