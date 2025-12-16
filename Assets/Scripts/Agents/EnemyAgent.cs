using UnityEngine;

public abstract class EnemyAgent : MonoBehaviour
{
    [SerializeField] protected Transform m_target;
    [SerializeField] protected float m_detectionRange;
    [SerializeField] protected float m_grabbingRange;
    [SerializeField] protected float m_patrollingRange;
    [SerializeField] protected float m_speed;
    [SerializeField] protected float m_rotationSpeed;
    [SerializeField] protected float m_scoreOnDeath;
    [SerializeField] protected float m_attackCooldown;

    protected int m_currentPatrolIndex = 0;
    protected bool m_patrolPointsSet;

    public Vector3[] PatrolPoints { get; set; }
    public float Speed { get { return m_speed; } }
    public float RotationSpeed { get { return m_rotationSpeed; } }
    public Transform Target { get { return m_target; } }
    public float PatrollingRange { get { return m_patrollingRange; } }
    public float Health { get; set; }
    public float Damage { get; set; }
    public float AttackTimer { get; set; }
    public float AttackCooldown { get { return m_attackCooldown; } }

    public void TakeDamage()
    {
        Health--;
    }

    protected Node.Status Patrol()
    {
        if (!m_patrolPointsSet)
        {
            SetPatrolPoints();
            m_patrolPointsSet = true;
        }
        if (PatrolPoints == null || PatrolPoints.Length == 0) return Node.Status.Failure;

        Vector3 target = PatrolPoints[m_currentPatrolIndex];

        Vector3 direction = (target - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                Time.deltaTime * RotationSpeed);
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            Speed * Time.deltaTime
        );

        if (transform.position == target)
        {
            m_currentPatrolIndex++;

            if (m_currentPatrolIndex >= PatrolPoints.Length)
            {
                m_currentPatrolIndex = 0;
            }
        }
        return Node.Status.Running;
    }

    protected Node.Status Chase()
    {
        if (!HasDetectedTarget())
            return Node.Status.Failure;

        Vector3 targetPosition = new(Target.transform.position.x, transform.position.y, Target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position,
            targetPosition, Speed * Time.deltaTime);

        Vector3 targetDirection = (Target.transform.position - transform.position).normalized;

        if (targetDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                Time.deltaTime * RotationSpeed);
        }
        m_patrolPointsSet = false;
        return Node.Status.Running;
    }

    protected Node.Status Grab()
    {
        if (!HasDetectedTarget() || !InGrabbingRange())
            return Node.Status.Failure;

        AttackTimer += Time.deltaTime;
        if (AttackTimer >= AttackCooldown)
        {
            if (Target.TryGetComponent<StatusController>(out var statusController))
            {
                statusController.TakeDamage(this);
                AttackTimer = 0f;
            }
        }
        return Node.Status.Running;
    }

    protected bool HasDetectedTarget()
    {
        float distance = Vector3.Distance(transform.position, m_target.transform.position);
        return distance < m_detectionRange;
    }

    protected bool InGrabbingRange()
    {
        float distance = Vector3.Distance(transform.position, m_target.transform.position);
        return distance < m_grabbingRange;
    }

    private void SetPatrolPoints()
    {
        PatrolPoints[0] = transform.position;
        for (int i = 1; i < PatrolPoints.Length; i++)
        {
            PatrolPoints[i] =
                PatrolPoints[i - 1] + new Vector3(Random.Range(-PatrollingRange, PatrollingRange),
                0f, Random.Range(-PatrollingRange, PatrollingRange));
        }
    }
}
