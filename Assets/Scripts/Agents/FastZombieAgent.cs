using UnityEngine;

public class FastZombieAgent : EnemyAgent
{
    private ConditionNode m_rootNode;
    private int m_currentPatrolIndex = 0;
    private bool m_patrolPointsSet;

    #region Unity Method
    void Awake()
    {
        PatrolPoints = new Vector3[4];
        Health = 1;
        Damage = 10;
        m_patrolPointsSet = false;

        void patrolAction() => Patrol();
        void chaseAction() => Chase();
        void grabAction() => Grab();

        ActionNode patrolActionNode = new(patrolAction);
        ActionNode chaseActionNode = new(chaseAction);
        ActionNode grabActionNode = new(grabAction);

        ConditionNode hasDetectedTarget = new(() => HasDetectedTarget(), chaseActionNode, patrolActionNode);
        ConditionNode inGrabbingRange = new(() => InGrabbingRange(), grabActionNode, hasDetectedTarget);

        m_rootNode = inGrabbingRange;
    }
    void Update()
    {
        m_rootNode.Evaluate();
        if (Health <= 0)
        {
            if (m_target.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                player.IncreaseScore(m_scoreOnDeath);
            }
            Health = 1;
            gameObject.SetActive(false);
        }
    }
    #endregion

    private void Patrol()
    {
        if (!m_patrolPointsSet)
        {
            SetPatrolPoints();
            m_patrolPointsSet = true;
        }
        if (PatrolPoints == null || PatrolPoints.Length == 0) return;

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

    private void Chase()
    {
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
    }

    private void Grab()
    {
        AttackTimer += Time.deltaTime;
        if (AttackTimer >= AttackCooldown)
        {
            if (Target.TryGetComponent<StatusController>(out var statusController))
            {
                statusController.TakeDamage(this);
                AttackTimer = 0f;
            }
        }
    }
}

