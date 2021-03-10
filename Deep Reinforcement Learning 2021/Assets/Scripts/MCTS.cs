using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


struct states
{
    public int[] board;
    public int N;
    public int Returns;

    public states(int[,] newBoard = null, int newN = 0, int newReturns = 0)
    {
        board = new int[]{0, 0, 0,
                           0, 0, 0,
                           0, 0, 0};

        N = newN;
        Returns = newReturns;
    }
    
    public void printState()
    {
        string status = "";
        for (int i = 1; i < board.Length +1; i++)
        {
            status += board[i - 1].ToString();
            
            if (i % 3 == 0)
            {
                status += "\n";
            }
        }

        Debug.Log(status);
    }

}

struct gameStateTicTacToe
{
    public List<int> choicesLeft;
    private List<CubeManager> board;
    private states actualState;
    private bool isBluePlaying;
    public bool isGameOver;

    
    void initGame()
    {
        choicesLeft.Clear();
        for (int i = 0; i < board.Count; i++)
        {
            choicesLeft.Add(i);
            board[i].isColored = false;
            board[i].isBlue = false;
            board[i].isRed = false;
        }
    }
    
    bool isBoardEqual(states a, states b)
    {
        for (int i = 0; i < a.board.Length; i++)
            if (a.board[i] != b.board[i]) 
                return false;
        return true;
    }
    
    public gameStateTicTacToe(List<int> possibleActions, List<CubeManager> newBoard)
    {
        actualState = new states(null, 0, 0);
        choicesLeft = possibleActions;
        board = newBoard;
        isBluePlaying = true;
        isGameOver = false;
    }

    public void next(int indexOfAction, List<states> listStates)
    {
        if (isGameOver)
        {
            initGame();
            isGameOver = false;
            for (int i = 0; i < listStates.Count; i++)
            {
               for (int j = 0; j < listStates.Count; j++)
               {
                   if (isBoardEqual(listStates[i], listStates[j]))
                   {
                       states tmpStates = listStates[i];
                       tmpStates.N++;
                       listStates.RemoveAt(i);
                   }
               } 
            }
        }
        
        if (board[choicesLeft[indexOfAction]].isColored == false)
        {
            if (isBluePlaying)
            {
                board[choicesLeft[indexOfAction]].playerOnePlaying(true);
                isBluePlaying = false;
                actualState.board[choicesLeft[indexOfAction]] = 1;
            }
            else
            {
                board[choicesLeft[indexOfAction]].playerTwoPlaying(true);
                isBluePlaying = true;
                actualState.board[choicesLeft[indexOfAction]] = 2;
            }

            listStates.Add(actualState);
            choicesLeft.RemoveAt(indexOfAction);
            actualState.printState();
        }
    }


    public string getBoardStatus()
    {
        string status = "\n";
        for (int i = 1; i < board.Count+1; i++)
        {
            if (board[i - 1].isRed)
            {
                status += "O";
            }
            else if (board[i - 1].isBlue)
            {
                status += "X";
            }
            else
            {
                status += "_";
            }
            
            if (i % 3 == 0)
            {
                status += "\n";
            }
        }

        return status;
    }
    
}

public class MCTS : MonoBehaviour
{

    
    gameStateTicTacToe testGS;
    private List<states> _boardStates = new List<states>();
    public bool isGameOver = false;

    public void initMCTS(List<int> possibleActions, List<CubeManager> newBoard)
    {
        _boardStates.Add(new states());
        testGS = new gameStateTicTacToe(possibleActions, newBoard);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int choosenIndex = Random.Range(0, testGS.choicesLeft.Count);
            testGS.next(choosenIndex, _boardStates);
            print(testGS.getBoardStatus());
        }

        if (isGameOver)
        {
            testGS.isGameOver = true;
            isGameOver = false;
        }
    }

    void computeMCTS()
    {
        int num_episodes = 10;
        int Ns = 0;
        int Returns = 0;

        for (int e = 0; e < num_episodes; e++)
        {
            
        }

    }
}
