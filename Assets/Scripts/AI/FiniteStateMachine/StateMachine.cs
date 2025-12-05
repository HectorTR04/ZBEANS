using Assets.Scripts.AI.FiniteStateMachine;
using UnityEngine;

public class StateMachine
{
    private IBombState currentState;

    void Start()
    {
        currentState = new PatrolState();
        currentState.Enter();
    }

    void Update()
    {
        currentState.Update();
    }

    public void TransitionToNewState(IBombState nextState)
    {
        currentState.Exit();

        currentState = nextState;
        currentState.Enter();
    }
}
