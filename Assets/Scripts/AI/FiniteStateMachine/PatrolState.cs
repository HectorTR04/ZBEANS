using Assets.Scripts.AI.FiniteStateMachine;
using UnityEngine;

public class PatrolState : State
{
    private Vector3 m_lastPosition;

    private int m_currentPatrolIndex = 0;

    public PatrolState(BombAgent agent, Vector3 lastPosition)
    {
        m_agent = agent;
        m_lastPosition = lastPosition;
    }
    public override void Enter()
    {
        SetPatrolPoints();
        //Debug.Log($"[{m_agent.gameObject.name}] Entered Patrol State");
    }

    public override void Update()
    {
        if (m_agent.PatrolPoints == null || m_agent.PatrolPoints.Length == 0)
            return;

        Vector3 target = m_agent.PatrolPoints[m_currentPatrolIndex];

        Vector3 direction = (target - m_agent.transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            m_agent.transform.rotation = Quaternion.Slerp(
                m_agent.transform.rotation,
                lookRotation,
                Time.deltaTime * m_agent.RotationSpeed);
        }

        m_agent.transform.position = Vector3.MoveTowards(
            m_agent.transform.position,
            target,
            m_agent.Speed * Time.deltaTime
        );

        if (m_agent.transform.position == target)
        {
            m_currentPatrolIndex++;

            if (m_currentPatrolIndex >= m_agent.PatrolPoints.Length)
            {
                m_currentPatrolIndex = 0;
            }
        }
    }

    public override void Exit()
    {
        //Debug.Log($"[{m_agent.gameObject.name}] Left Patrol State");
    }

    private void SetPatrolPoints()
    {
        m_agent.PatrolPoints[0] = m_lastPosition;
        for(int i = 1; i < m_agent.PatrolPoints.Length; i++)
        {
            m_agent.PatrolPoints[i] = 
                m_agent.PatrolPoints[i-1] + new Vector3(Random.Range(-m_agent.PatrollingRange, m_agent.PatrollingRange), 
                0f, Random.Range(-m_agent.PatrollingRange, m_agent.PatrollingRange));
        }
    }
}
