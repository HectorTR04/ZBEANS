using UnityEngine;

public class FastZombieAgent : EnemyAgent
{
    private ConditionNode m_rootNode;

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
}

