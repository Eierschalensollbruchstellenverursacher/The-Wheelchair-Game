using System;
using System.Collections.Generic;

namespace GameplayFSM
{
    public sealed class GameplayFSM : FSM<GameplayFSMState>
    {
        public override GameplayFSMState CurrentState { get; protected set; }
        protected override Dictionary<Type, GameplayFSMState> States { get; set; }

        public override void AddState(GameplayFSMState state)
        {
            if (States.ContainsKey(state.GetType()))
                throw new InvalidOperationException();

            States.Add(state.GetType(), state);
        }

        public override void SetState<TState>()
        {
            Type newStateType = typeof(TState);

            if (CurrentState != null && newStateType == CurrentState.GetType())
                return;

            CurrentState?.Exit();
            CurrentState = States[newStateType];
            CurrentState.Enter();
        }

        public override void Update() => CurrentState?.Update();  
    }
}
