using System;
using System.Collections;
using UnityEngine;

namespace FallingObstacles.InternalLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class FallingObstacle : MonoBehaviour
    {
        [SerializeField] private FallingObstacleConfig _config;


        public FallingObstacleConfig Config
        {
            get => _config;
            set => _config = _config == null
                ? value
                : throw new InvalidOperationException(
                    $"Cannot assign value to '{gameObject.name}' object.\n" +
                    $"{nameof(_config)} field already has an assigned value"
                    );
        }


        private void OnEnable() =>
            StartCoroutine(DespawnAfterDelay(_config.DespawnDelay));

        private IEnumerator DespawnAfterDelay(float timeSeconds)
        {
            yield return new WaitForSeconds(timeSeconds);

            // Place despawn logic here...
            // like...
            FallingObstacleFactory.ReturnToPool(this);
        }
    }
}