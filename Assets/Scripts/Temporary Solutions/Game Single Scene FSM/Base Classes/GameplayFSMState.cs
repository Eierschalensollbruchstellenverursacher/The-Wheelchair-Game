using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameplayFSM
{
    public abstract class GameplayFSMState : FSMState<GameplayFSM>
    {
        public GameplayFSMState(GameplayFSM stateMachine)
            : base(stateMachine)
        {
            ThisStateOnlyObjects = new List<GameObject>();
        }

        protected GameplayFSMState(GameplayFSM stateMachine, IEnumerable<GameObject> thisStateOnlyObjects)
            : this(stateMachine)
        {
            ThisStateOnlyObjects = thisStateOnlyObjects.ToList();
        }

        protected List<GameObject> ThisStateOnlyObjects { get; set; }

        public new virtual void Enter()
        {
            base.Enter();

            foreach (GameObject @object in ThisStateOnlyObjects)
                @object.SetActive(true);
        }

        public new virtual void Exit()
        {
            base.Exit();

            foreach (GameObject @object in ThisStateOnlyObjects)
                @object.SetActive(false);
        }

        public new virtual void Update()
        {
            base.Update();
        }
    }
}