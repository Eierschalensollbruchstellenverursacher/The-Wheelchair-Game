using UnityEngine;

namespace FallingObstacles.InternalLogic
{
    [CreateAssetMenu(fileName = "FallingObstacle", menuName = "Obstacles/Falling Obstacle")]
    public sealed class FallingObstacleConfig : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private ObstacleTypes _type;

        [Tooltip("Leave the field at 0 if you don't want objects of this type to despawn automatically.")]
        [SerializeField, Min(0f)] private float _despawnDelay;

        public ObstacleTypes Type => _type;
        public float DespawnDelay => _despawnDelay;

        public FallingObstacle Create()
        {
            GameObject obstacle = Instantiate(_prefab);
            obstacle.SetActive(false);
            obstacle.name = _prefab.name;

            FallingObstacle obstacleComponent = obstacle.AddComponent<FallingObstacle>();
            obstacleComponent.Config = this;

            return obstacleComponent;
        }

        public void OnGet(FallingObstacle obstacle) =>
            obstacle.gameObject.SetActive(true);

        public void OnRelease(FallingObstacle obstacle) =>
            obstacle.gameObject.SetActive(false);

        public void OnDestroyPoolObject(FallingObstacle obstacle) =>
            Destroy(obstacle.gameObject);
    }
}