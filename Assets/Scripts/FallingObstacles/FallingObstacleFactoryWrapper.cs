using System;
using System.Collections.Generic;
using FallingObstacles.InternalLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FallingObstacles
{
    public sealed class FallingObstacleFactoryWrapper : MonoBehaviour
    {
        public static Action OnFallingObjectsSpawn;

        [SerializeField] internal List<FallingObstacleConfig> _spawnedObstacleConfigs = new();

        [SerializeField, Min(0)] private int _spawnQuantity = 5;
        [Space]
        [SerializeField] private float _minUpSpawnDistance = 8f;
        [SerializeField] private float _maxUpSpawnDistance = 10f;
        [Space]
        [SerializeField] private float _maxSpawnIndentLeft  = -0.85f;
        [SerializeField] private float _maxSpawnIndentRight = 0.85f;
        
        private CustomXRGrabInteractable grabInteractable;
        public void Initialize(CustomXRGrabInteractable grabInteractable)
        {
            // Set up the reference to the CustomXRGrabInteractable script
            this.grabInteractable = grabInteractable;
        }

        [ContextMenu("Spawn Obstacle")]
        public void SpawnObstacle()
        {
            FallingObstacleConfig spawnedObstacleConfig = GetRandomObstacleConfig();

            OnFallingObjectsSpawn?.Invoke();

            for (int i = 0; i < _spawnQuantity; i++)
            {
                Vector3 spawnPosition = new Vector3(
                    transform.position.x + Random.Range(_maxSpawnIndentLeft, _maxSpawnIndentRight),
                    transform.position.y + Random.Range(_minUpSpawnDistance, _maxUpSpawnDistance),
                    transform.position.z
                    );

                ProduceAt(spawnedObstacleConfig, spawnPosition, setActiveAutomatically: true);
            }
        }

        public FallingObstacleConfig GetRandomObstacleConfig()
        {
            if (_spawnedObstacleConfigs.Count <= 1)
                return _spawnedObstacleConfigs[0];

            int obstacleIndex = Random.Range(0, _spawnedObstacleConfigs.Count);
            FallingObstacleConfig spawnedObstacleConfig = _spawnedObstacleConfigs[obstacleIndex];

            return spawnedObstacleConfig;
        }

        public FallingObstacle ProduceAt(FallingObstacleConfig spawnedObstacleConfig, Vector3 position, Quaternion initialRotation, bool setActiveAutomatically = false)
        {
            FallingObstacle obstacle = FallingObstacleFactory.Spawn(spawnedObstacleConfig);
            obstacle.transform.position = position;
            obstacle.transform.rotation = initialRotation;
            obstacle.gameObject.SetActive(setActiveAutomatically);

            return obstacle;
        }

        public FallingObstacle ProduceAt(FallingObstacleConfig spawnedObstacleConfig, Vector3 position, bool setActiveAutomatically = false)
            => ProduceAt(spawnedObstacleConfig, position, Quaternion.identity, setActiveAutomatically);

        public FallingObstacle ProduceAt(FallingObstacleConfig spawnedObstacleConfig, bool setActiveAutomatically = false)
            => ProduceAt(spawnedObstacleConfig, Vector3.zero, setActiveAutomatically);



        private void Update()
        {
            if (_maxUpSpawnDistance < _minUpSpawnDistance)
                _minUpSpawnDistance = _maxUpSpawnDistance;

            if (_maxSpawnIndentRight < _maxSpawnIndentLeft)
                _maxSpawnIndentRight = _maxSpawnIndentLeft;
        }
    }
}