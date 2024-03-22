#define LOGGING_STATES

using UnityEngine;
using System.Collections.Generic;

namespace GameplayFSM
{
    public sealed class GameplayStateMachine : MonoBehaviour
    {
        [Header("Objects for only one state: ")]
        [SerializeField] List<GameObject> _menuStateObjects = new();
        [SerializeField] List<GameObject> _playingStateObjects = new();
        [SerializeField] List<GameObject> _pausedStateObjects = new();
        [SerializeField] List<GameObject> _diedStateObjects = new();
        [SerializeField] List<GameObject> _wonStateObjects = new();

        public static GameplayStateMachine Instance { get; private set; }

        public GameplayFSM StateMachine { get; private set; } = new();

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"Unable to create '{gameObject.name}'. Another instance of '{nameof(GameplayStateMachine)}' type exists on this scene.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            StateMachine.AddState(new GameplayStateInMenu(StateMachine, _menuStateObjects));
            StateMachine.AddState(new GameplayStatePlaying(StateMachine, _playingStateObjects));
            StateMachine.AddState(new GameplayStatePaused(StateMachine, _pausedStateObjects));
            StateMachine.AddState(new GameplayStateWon(StateMachine, _wonStateObjects));
            StateMachine.AddState(new GameplayStateDead(StateMachine, _diedStateObjects));

            StateMachine.SetState<GameplayStateInMenu>();
        }

#if UNITY_EDITOR && LOGGING_STATES
        private GameplayFSMState _currentState;

        private void LateUpdate()
        {
            if (_currentState is null || _currentState != StateMachine.CurrentState)
            {
                _currentState = StateMachine.CurrentState;
                Debug.Log($"Entered {_currentState} state");
            }
        }
#endif
    }
}

