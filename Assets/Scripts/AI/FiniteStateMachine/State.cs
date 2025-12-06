namespace Assets.Scripts.AI.FiniteStateMachine
{
    public abstract class State
    {
        protected BombAgent m_agent;

        public abstract void Enter();

        public abstract void Update();

        public abstract void Exit();
    }
}
