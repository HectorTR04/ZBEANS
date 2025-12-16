using System;

public class Condition : Node
{
    readonly Func<bool> m_predicate;

    public Condition(string name, Func<bool> predicate) : base(name)
    {
        m_predicate = predicate;
    }

    public override Status Process() => m_predicate() ? Status.Success : Status.Failure;
}


