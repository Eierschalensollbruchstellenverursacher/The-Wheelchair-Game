using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFSM
{
    public sealed class GameplayStateInMenu : GameplayFSMState
    {
        public GameplayStateInMenu(GameplayFSM stateMachine)
                : base(stateMachine) { }

        public GameplayStateInMenu(GameplayFSM stateMachine, IEnumerable<GameObject> thisStateOnlyObjects)
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


