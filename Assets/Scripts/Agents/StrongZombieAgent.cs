using UnityEngine;

public class StrongZombieAgent : EnemyAgent
{
    private BehaviourTree m_behaviourTree;

    void Awake()
    {
        PatrolPoints = new Vector3[4];
        Health = 3;
        Damage = 20;

        m_behaviourTree = new BehaviourTree("StrongZombie");

        Selector root = new("Root");

        Sequence grabSequence = new("GrabSequence");
        Sequence chaseSequence = new("ChaseSequence");

        Condition playerInRange = new("PlayerInRange", HasDetectedTarget);
        Condition canGrabPlayer = new("CanGrabPlayer", InGrabbingRange);
        OperationNode grabPlayer = new("GrabPlayer", Grab, null);
        OperationNode chasePlayer = new("ChasePlayer", Chase, null);
        OperationNode patrol = new("Patrol", Patrol, null);
        
        grabSequence.AddChild(playerInRange);
        grabSequence.AddChild(canGrabPlayer);
        grabSequence.AddChild(grabPlayer);

        chaseSequence.AddChild(playerInRange);
        chaseSequence.AddChild(chasePlayer);
        
        root.AddChild(grabSequence);
        root.AddChild(chaseSequence);
        root.AddChild(patrol);

        m_behaviourTree.AddChild(root);
    }

    void Update()
    {
        m_behaviourTree.Process();
        if (Health <= 0)
        {
            if (m_target.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                player.IncreaseScore(m_scoreOnDeath);
            }
            Health = 3;
            gameObject.SetActive(false);
        }
    }
}
