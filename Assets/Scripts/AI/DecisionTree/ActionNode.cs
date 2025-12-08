using System;
public class ActionNode : IDecisionNode
{
    private readonly Action m_action;

    public ActionNode(Action action)
    {
        m_action = action;
    }

    public void Evaluate()
    {
        m_action();
    }
}
