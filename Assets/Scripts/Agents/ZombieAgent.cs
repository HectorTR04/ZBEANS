using UnityEngine;

[RequireComponent (typeof(StateMachine))]
public class ZombieAgent : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_detectionRange;
    [SerializeField] private float m_grabbingRange;
    [SerializeField] private float m_patrollingRange;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_rotationSpeed;
    [SerializeField] private float m_scoreOnDeath;

    [SerializeField] private float m_attackCooldown;

    public Vector3[] PatrolPoints { get; set; }
    public float Speed { get { return m_speed; } }
    public float RotationSpeed { get { return m_rotationSpeed; } }
    public Transform Target { get { return m_target; } }
    public float PatrollingRange { get { return m_patrollingRange; } }
    public float Health { get; set; }
    public float Damage { get; set; }
    public float AttackTimer { get; set; }
    public float AttackCooldown { get  { return m_attackCooldown; } }

    private StateMachine m_stateMachine;

    #region Unity Method
    void Start()
    {
        PatrolPoints = new Vector3[4];
        m_stateMachine = GetComponent<StateMachine>();
        Health = 3;
        Damage = 10;
    }
    void Update()
    {
        if (!HasDetectedTarget() && m_stateMachine.CurrentState is not PatrolState)
        {
            m_stateMachine.TransitionToNewState(new PatrolState(this, transform.position));
        }
        if (HasDetectedTarget() && m_stateMachine.CurrentState is not ChaseState && !InGrabbingRange())
        {
            m_stateMachine.TransitionToNewState(new ChaseState(this));
        }
        if (InGrabbingRange() && m_stateMachine.CurrentState is not GrabbingState)
        {
            m_stateMachine.TransitionToNewState(new GrabbingState(this));
        }
        if(Health <= 0)
        {
            PlayerController player = m_target.gameObject.GetComponent<PlayerController>();
            if(player != null)
            {
                player.IncreaseScore(m_scoreOnDeath);
            }
            gameObject.SetActive(false);
        }
    }
    #endregion
    public void TakeDamage()
    {
        Health--;
    }

    private bool HasDetectedTarget()
    {
        float distance = Vector3.Distance(transform.position, m_target.transform.position);
        return distance < m_detectionRange;
    }

    private bool InGrabbingRange()
    {
        float distance = Vector3.Distance(transform.position, m_target.transform.position);
        return distance < m_grabbingRange;
    }
}
