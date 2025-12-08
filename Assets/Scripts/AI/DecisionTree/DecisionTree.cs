using UnityEngine;

public class DecisionTree : MonoBehaviour
{
    private ZombieAgent m_agent;
    private DecisionNode rootNode;

    void Start()
    {
        m_agent = GetComponent<ZombieAgent>();
    }

    void Update()
    {
        rootNode.Evaluate();
    }

    
}
