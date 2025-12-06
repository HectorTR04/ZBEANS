using UnityEngine;

[RequireComponent (typeof(StateMachine))]
public class BombAgent : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_detectionRange;
    [SerializeField] private float m_explodingRange;
    [SerializeField] private float m_patrollingRange;
    [SerializeField] private float m_speed;

    public bool IsExploding { get; set; }
    public Vector3[] PatrolPoints { get; set; }
    public float Speed { get { return m_speed; } }
    public Transform Target { get { return m_target; } }
    public float PatrollingRange { get { return m_patrollingRange; } }

    private StateMachine m_stateMachine;

    void Start()
    {
        m_stateMachine = GetComponent<StateMachine>();
        IsExploding = false;
    }

    void Update()
    {
        if (!HasDetectedTarget() && m_stateMachine.CurrentState is not PatrolState)
        {
            m_stateMachine.TransitionToNewState(new PatrolState(this, transform.position));
        }
        if (HasDetectedTarget() && m_stateMachine.CurrentState is not ChaseState && !InExplodingRange())
        {
            m_stateMachine.TransitionToNewState(new ChaseState(this));
        }
        if (InExplodingRange() && m_stateMachine.CurrentState is not ExplodingState)
        {
            m_stateMachine.TransitionToNewState(new ExplodingState(this));
        }
    }

    private bool HasDetectedTarget()
    {
        float distance = Vector3.Distance(transform.position, m_target.transform.position);
        return distance < m_detectionRange;
    }

    private bool InExplodingRange()
    {
        float distance = Vector3.Distance(transform.position, m_target.transform.position);
        return distance < m_explodingRange;
    }
}
