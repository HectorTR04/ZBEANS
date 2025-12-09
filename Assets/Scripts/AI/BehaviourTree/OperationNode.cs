using System;

public class OperationNode : Node
{
    private readonly Action m_process;
    private readonly Action m_reset;
    public OperationNode(string name, Action process, Action reset) : base(name)
    {
        m_process = process;
        m_reset = reset;
    }

    public override Status Process()
    {
        m_process();
        return Status.Success;  
    }

    public override void Reset()
    {
        m_reset(); 
    }
}

