using Assets.Scripts.AI.FiniteStateMachine;
using UnityEngine;

public class PatrolState : State
{
    private Vector3 m_lastPosition;

    private Vector3 m_firstPatrolPoint;
    private Vector3 m_secondPatrolPoint;
    private Vector3 m_thirdPatrolPoint;
    private int m_currentPatrolIndex = 0;

    public PatrolState(BombAgent agent, Vector3 lastPosition)
    {
        m_agent = agent;
        m_lastPosition = lastPosition;
    }
    public override void Enter()
    {
        SetPatrolPoints();
        Debug.Log($"[{m_agent.gameObject.name}] Entered Patrol State");
    }

    public override void Update()
    {
        if (m_agent.PatrolPoints == null || m_agent.PatrolPoints.Length == 0)
            return;

        Vector3 target = m_agent.PatrolPoints[m_currentPatrolIndex];

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
        m_firstPatrolPoint = Vector3.zero;
        m_secondPatrolPoint = Vector3.zero;
        m_thirdPatrolPoint = Vector3.zero;
        Debug.Log($"[{m_agent.gameObject.name}] Left Patrol State");
    }

    private void SetPatrolPoints()
    {
        m_firstPatrolPoint = m_lastPosition;

        m_secondPatrolPoint = m_firstPatrolPoint 
            + new Vector3(Random.Range(-m_agent.PatrollingRange, m_agent.PatrollingRange), 0f, Random.Range(-m_agent.PatrollingRange, m_agent.PatrollingRange));

        m_thirdPatrolPoint = m_secondPatrolPoint 
            + new Vector3(Random.Range(-m_agent.PatrollingRange, m_agent.PatrollingRange), 0f, Random.Range(-m_agent.PatrollingRange, m_agent.PatrollingRange));

        m_agent.PatrolPoints = new[] { m_firstPatrolPoint, m_secondPatrolPoint, m_thirdPatrolPoint };
    }
}
