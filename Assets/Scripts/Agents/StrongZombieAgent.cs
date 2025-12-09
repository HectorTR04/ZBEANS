using UnityEngine;

public class StrongZombieAgent : EnemyAgent
{
    private BehaviourTree m_behaviourTree;

    void Awake()
    {
        PatrolPoints = new Vector3[4];
        Health = 1;
        Damage = 10;






    }

    void Update()
    {
        m_behaviourTree.Process();
    }
}
