using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Obstacles
{
    public sealed class FallingObstacleFactory : MonoBehaviour
    {
        [SerializeField] private int _defaultCapacity = 10;
        [SerializeField] private int _maxPoolSize = 15;
        
        private bool _collectionCheck = true;

        private readonly Dictionary<ObstacleTypes, IObjectPool<FallingObstacle>> _pools = new();

        private static FallingObstacleFactory _instance;
        public static FallingObstacleFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject factoryObject = new GameObject("[Falling Obstacles Factory]");
                    DontDestroyOnLoad(factoryObject);

                    _instance = factoryObject.AddComponent<FallingObstacleFactory>();
                }

                return _instance;
            }
            private set => _instance = value;   
        }

        public IObjectPool<FallingObstacle> GetPoolFor(FallingObstacleConfig config)
        {
            IObjectPool<FallingObstacle> pool;

            if (_pools.TryGetValue(config.Type, out pool) == true)
                return pool;

            pool = new UnityEngine.Pool.ObjectPool<FallingObstacle>(
                config.Create,
                config.OnGet,
                config.OnRelease,
                config.OnDestroyPoolObject,
                _collectionCheck,
                _defaultCapacity,
                _maxPoolSize
                );

            Debug.Log($"Pool created for {config.name}");

            _pools.Add(config.Type, pool);
            return pool;
        }

        public static FallingObstacle Spawn(FallingObstacleConfig config) =>
            Instance.GetPoolFor(config)?.Get();

        public static void ReturnToPool(FallingObstacle obstacle) =>
            Instance.GetPoolFor(obstacle.Config)?.Release(obstacle);

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError(
                    $"Failed to create '{gameObject.name}' object. " +
                    $"Another instance of {nameof(FallingObstacleFactory)} " +
                    $"type exists on the scene."
                    );
                Destroy(gameObject);

                return;
            }

            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
}