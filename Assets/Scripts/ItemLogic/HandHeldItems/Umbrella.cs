using System.Collections;
using Obstacles;
using UnityEngine;

namespace ItemLogic.HandHeldItems
{
    public class Umbrella : MonoBehaviour
    {
        [SerializeField] private float destroyAfterDelay = 5f;

        private void Start()
        {
            // Start the coroutine to destroy this object after a specified delay
            StartCoroutine(DestroyAfterDelay(destroyAfterDelay));
        }

        // Coroutine to destroy the game object after a delay
        private IEnumerator DestroyAfterDelay(float delay)
        {
            // Wait for the time defined in 'delay'
            yield return new WaitForSeconds(delay);
            Destroy(this.gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Check if the object collided with has a component of type FallingObstacle
            if (collision.gameObject.TryGetComponent<FallingObstacle>(out _))
            {
                // If it does, destroy the collided object
                Destroy(collision.gameObject);
            }
        }
    }
}