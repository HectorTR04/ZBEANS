using System;

public class ConditionNode : IDecisionNode
{
    private readonly Func<bool> m_condition;
    private readonly IDecisionNode m_trueNode;
    private readonly IDecisionNode m_falseNode;

    public ConditionNode(Func<bool> condition, IDecisionNode trueNode, IDecisionNode falseNode)
    {
        m_condition = condition;
        m_trueNode = trueNode;
        m_falseNode = falseNode;
    }

    public void Evaluate()
    {
        if (m_condition())
        {
            m_trueNode.Evaluate(); 
        }
        else
        {
            m_falseNode.Evaluate();
        }
    }
}
