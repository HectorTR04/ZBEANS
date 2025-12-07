using Assets.Scripts.AI.FiniteStateMachine;

public class GrabbingState : State
{
    public GrabbingState(ZombieAgent agent)
    {
        m_agent = agent;
    }
    public override void Enter()
    {
        //Debug.Log($"Entered Exploding State");
    }
    public override void Update()
    {
        m_agent.IsGrabbing = true;
    }
    public override void Exit()
    {
    }
}

