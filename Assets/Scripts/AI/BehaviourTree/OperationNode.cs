using System;
using UnityEngine;

public class OperationNode : Node
{
    private readonly Func<Status> m_process;
    private readonly Action m_reset;

    public OperationNode(string name, Func<Status> process, Action reset) : base(name)
    {
        m_process = process;
        m_reset = reset;
    }

    public override Status Process()
    {
        Debug.Log("in operation " + name);
        return m_process();
    }

    public override void Reset()
    {
        if(m_reset == null)
        {
            return;
        }
        m_reset();
    }
}

