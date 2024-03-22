using System.Collections.Generic;

public abstract class FSM<TMachineStates>
{
    public abstract TMachineStates CurrentState { get; protected set; }
    protected abstract Dictionary<System.Type, TMachineStates> States { get; set; }

    public abstract void AddState(TMachineStates state);
    public abstract void SetState<TState>() where TState : TMachineStates;
    public abstract void Update();
}
