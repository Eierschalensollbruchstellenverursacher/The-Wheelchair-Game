using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFSM
{
    public sealed class GameplayStatePaused : GameplayFSMState
    {
        public GameplayStatePaused(GameplayFSM stateMachine)
                : base(stateMachine) { }

        public GameplayStatePaused(GameplayFSM stateMachine, IEnumerable<GameObject> thisStateOnlyObjects)
                : base(stateMachine, thisStateOnlyObjects) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
