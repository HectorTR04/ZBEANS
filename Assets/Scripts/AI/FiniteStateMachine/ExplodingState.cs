using Assets.Scripts.AI.FiniteStateMachine;
using UnityEngine;

public class ExplodingState : State
{
    public ExplodingState(BombAgent agent)
    {
        m_agent = agent;
    }
    public override void Enter()
    {
        Debug.Log($"Entered Exploding State");
    }
    public override void Update()
    {
        if (!m_agent.IsExploding)
        {
            Explode();
        }
    }
    public override void Exit()
    {
        m_agent.gameObject.SetActive(false);
    }

    private void Explode()
    {
        m_agent.IsExploding = true;

        Exit();
    }
}

