using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


struct gameStateTicTacToe
{
    public List<int> choicesLeft;
    private List<CubeManager> board;
    private bool isBluePlaying;

    public gameStateTicTacToe(List<int> possibleActions, List<CubeManager> newBoard)
    {
        choicesLeft = possibleActions;
        board = newBoard;
        isBluePlaying = true;
    }

    public void next(int indexOfAction)
    {
        if (board[choicesLeft[indexOfAction]].isColored == false)
        {
            if (isBluePlaying)
            {
                board[choicesLeft[indexOfAction]].playerOnePlaying(true);
                isBluePlaying = false;
            }
            else
            {
                board[choicesLeft[indexOfAction]].playerTwoPlaying(true);
                isBluePlaying = true;
            }
            choicesLeft.RemoveAt(indexOfAction);
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
    private List<int> debugActions;
    public void initMCTS(List<int> possibleActions, List<CubeManager> newBoard)
    {
        debugActions = possibleActions;
        testGS = new gameStateTicTacToe(possibleActions, newBoard);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int choosenIndex = Random.Range(0, testGS.choicesLeft.Count);
            print(choosenIndex);
            testGS.next(choosenIndex);
            print(testGS.getBoardStatus());
        }
    }
}
