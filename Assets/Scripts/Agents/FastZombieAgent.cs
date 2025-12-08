using UnityEngine;

[RequireComponent (typeof(DecisionTree))]
public class FastZombieAgent : MonoBehaviour
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
    public float AttackCooldown { get { return m_attackCooldown; } }

    private DecisionTree m_decisionTree;

    #region Unity Method
    void Awake()
    {
        PatrolPoints = new Vector3[4];
        Health = 1;
        Damage = 10;
    }

    private void Update()
    {
        
    }
    #endregion


}

