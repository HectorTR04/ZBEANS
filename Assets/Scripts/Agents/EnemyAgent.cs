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
}
