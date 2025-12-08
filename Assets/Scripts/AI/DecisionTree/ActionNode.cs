using System;
public class ActionNode : DecisionNode
{
    private Action action;

    public ActionNode(Action action)
    {
        this.action = action;
    }

    public void Evaluate()
    {
        action();
    }
}
