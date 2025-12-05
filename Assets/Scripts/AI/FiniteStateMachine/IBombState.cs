namespace Assets.Scripts.AI.FiniteStateMachine
{
    public interface IBombState
    {
        void Enter();

        void Update();

        void Exit();
    }
}
