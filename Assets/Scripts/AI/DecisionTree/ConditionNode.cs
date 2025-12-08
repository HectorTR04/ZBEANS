using System;

public class ConditionNode : DecisionNode
{
    private Func<bool> condition;
    private DecisionNode trueNode;
    private DecisionNode falseNode;

    public ConditionNode(Func<bool> condition, DecisionNode trueNode, DecisionNode falseNode)
    {
        this.condition = condition;
        this.trueNode = trueNode;
        this.falseNode = falseNode;
    }

    public void Evaluate()
    {
        if (condition())
        {
            trueNode.Evaluate(); 
        }
        else
        {
            falseNode.Evaluate();
        }
    }
}
