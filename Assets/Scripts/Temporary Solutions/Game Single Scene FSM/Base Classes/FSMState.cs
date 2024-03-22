public abstract class FSMState<TStateMachine>
{
    protected readonly TStateMachine StateMachine;
    
    public FSMState(TStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update() { }
}