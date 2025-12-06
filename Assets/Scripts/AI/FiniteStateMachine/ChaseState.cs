using Assets.Scripts.AI.FiniteStateMachine;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(BombAgent agent)
    {
        m_agent = agent;
    }
    public override void Enter()
    {
        Debug.Log($"[{m_agent.gameObject.name}] Entered Chase State");
    }
    public override void Update()
    {
        Vector3 targetPosition = new(m_agent.Target.transform.position.x, m_agent.transform.position.y, m_agent.Target.transform.position.z);
        m_agent.transform.position = Vector3.MoveTowards(m_agent.transform.position,
            targetPosition, m_agent.Speed * Time.deltaTime);
    }

    public override void Exit()
    {
        Debug.Log($"[{m_agent.gameObject.name}] Left Chase State");
    }

}
   
