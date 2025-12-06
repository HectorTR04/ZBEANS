using Assets.Scripts.AI.FiniteStateMachine;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BombAgent m_agent;
    private State currentState;
    public State CurrentState { get { return currentState; } }

    void Start()
    {
        m_agent = GetComponent<BombAgent>();
        currentState = new PatrolState(m_agent, m_agent.transform.position);
        currentState.Enter();
    }

    void Update()
    {
        currentState.Update();
    }

    public void TransitionToNewState(State nextState)
    {
        currentState.Exit();

        currentState = nextState;
        currentState.Enter();
    }
}
