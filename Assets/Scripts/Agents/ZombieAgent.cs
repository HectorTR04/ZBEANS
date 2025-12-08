using UnityEngine;

[RequireComponent (typeof(StateMachine))]
public class ZombieAgent : EnemyAgent
{
    private StateMachine m_stateMachine;

    #region Unity Method
    void Awake()
    {
        PatrolPoints = new Vector3[4];
        m_stateMachine = GetComponent<StateMachine>();
        Health = 1;
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
            if(m_target.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                player.IncreaseScore(m_scoreOnDeath);
            }
            Health = 1;
            gameObject.SetActive(false);
        }
    }
    #endregion
}
