using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace GameplayFSM
{
    public sealed class GameplayFSM : FSM<GameplayFSMState>
    {
        public GameplayFSM()
        {
            States = new Dictionary<Type, GameplayFSMState>();
        }

        public override GameplayFSMState CurrentState { get; protected set; }
        protected override Dictionary<Type, GameplayFSMState> States { get; set; }

        public override void AddState(GameplayFSMState state)
        {
            //if (States.ContainsKey(state.GetType()))
            //    throw new InvalidOperationException();

            try
            {
                States.Add(state.GetType(), state);
            }
            catch(Exception e) { Debug.LogException(e); }

            Debug.Log($"{state} state added.");
        }

        public override void SetState<TState>()
        {
            Type newStateType = typeof(TState);

            if (CurrentState != null && newStateType == CurrentState.GetType())
                return;

            if (States.TryGetValue(newStateType, out GameplayFSMState newState))
            {
                CurrentState?.Exit();
                CurrentState = States[newStateType];
                CurrentState.Enter();
            }
        }

        public override void Update() => CurrentState?.Update();  
    }
}
