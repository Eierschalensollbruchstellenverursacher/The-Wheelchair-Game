using System.Collections.Generic;
using UnityEngine;

namespace GameplayFSM
{
    public sealed class GameplayStatePlaying : GameplayFSMState
    {
        public GameplayStatePlaying(GameplayFSM stateMachine)
                : base(stateMachine) { }

        public GameplayStatePlaying(GameplayFSM stateMachine, IEnumerable<GameObject> thisStateOnlyObjects)
                : base(stateMachine, thisStateOnlyObjects) { }


        public override void Enter()
        {
            Enemy.OnReachingTarget += SwitchToDeadState;
            base.Enter();
        }

        public override void Exit()
        {
            Enemy.OnReachingTarget -= SwitchToDeadState;
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
        }

        private void SwitchToDeadState() => StateMachine.SetState<GameplayStateDead>();
        private void SwitchToWonState() => StateMachine.SetState<GameplayStateWon>();
        private void SwitchToPausedState() => StateMachine.SetState<GameplayStatePaused>();

        

    }
}
