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

        private GameplayFSM _stateMachine= new();

        public static GameplayStateMachine Instance { get; private set; }


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
            _stateMachine.AddState(new GameplayStateInMenu(_stateMachine, _menuStateObjects));
            _stateMachine.AddState(new GameplayStatePlaying(_stateMachine, _playingStateObjects));
            _stateMachine.AddState(new GameplayStatePaused(_stateMachine, _pausedStateObjects));
            _stateMachine.AddState(new GameplayStateWon(_stateMachine, _wonStateObjects));
            _stateMachine.AddState(new GameplayStateDead(_stateMachine, _diedStateObjects));

            _stateMachine.SetState<GameplayStateInMenu>();
        }

#if UNITY_EDITOR && LOGGING_STATES
        private GameplayFSMState _currentState;

        private void LateUpdate()
        {
            if (_currentState is null || _currentState != _stateMachine.CurrentState)
            {
                _currentState = _stateMachine.CurrentState;
                Debug.Log($"Entered {_currentState} state");
            }
        }
#endif
    }
}

