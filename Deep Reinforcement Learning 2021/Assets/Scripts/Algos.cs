using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum codeAction
{
    action0, 
    action1, 
    action2, 
    action3, 
    action4,
    action5,
    action6,
    action7,
    action8
}

public class Action
{

    public codeAction idAction;
    public State stateDestination;

    public Action(codeAction unId, State unEtat)
    {
        idAction = unId;
        stateDestination = unEtat;
    }

}

public abstract class State
{

    protected List<Action> actions = new List<Action>();

   public State(List<Action> lesActions)
    {

        actions = lesActions;

    }

    public State()
    {

    }

}

class GridWorldState : State
{

    public int x;
    public int y;

    public GridWorldState(List<Action> lesActions, int posX, int posY)
    {

        actions = lesActions;
        x = posX;
        y = posY;

    }

}

    public class Algos : MonoBehaviour
{

    List<State> listeEtats = new List<State>();

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
