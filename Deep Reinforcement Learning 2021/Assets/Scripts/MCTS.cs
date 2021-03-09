using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

struct gameState
{
    private List<int> choicesLeft;
    private List<List<CubeManager>> board;

    public gameState(List<int> possibleActions, List<List<CubeManager>> newBoard)
    {
        choicesLeft = possibleActions;
        board = newBoard;
    }
}

public class MCTS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
