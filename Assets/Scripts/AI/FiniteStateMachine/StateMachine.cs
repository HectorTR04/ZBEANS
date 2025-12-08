using Assets.Scripts.AI.FiniteStateMachine;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private ZombieAgent m_agent;
    private State currentState;
    public State CurrentState { get { return currentState; } }

    #region Unity Methods
    void Start()
    {
        m_agent = GetComponent<ZombieAgent>();
        currentState = new PatrolState(m_agent, m_agent.transform.position);
        currentState.Enter();
    }

    void Update()
    {
        currentState.Update();
    }
    #endregion

    public void TransitionToNewState(State nextState)
    {
        currentState.Exit();

        currentState = nextState;
        currentState.Enter();
    }
}
