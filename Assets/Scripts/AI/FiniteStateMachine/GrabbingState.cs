using Assets.Scripts.AI.FiniteStateMachine;
using UnityEngine;

public class GrabbingState : State
{
    public GrabbingState(ZombieAgent agent)
    {
        m_agent = agent;
    }
    public override void Enter()
    {
        m_agent.AttackTimer = 0f;
    }
    public override void Update()
    {
        m_agent.AttackTimer += Time.deltaTime;
        if (m_agent.AttackTimer >= m_agent.AttackCooldown)
        {
            if (m_agent.Target.TryGetComponent<StatusController>(out var statusController))
            {
                statusController.TakeDamage(m_agent);
                m_agent.AttackTimer = 0f;
            }
        }
    }
    public override void Exit()
    {
    }
}

