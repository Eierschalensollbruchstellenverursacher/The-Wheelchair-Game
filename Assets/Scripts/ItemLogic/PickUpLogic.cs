using System.Collections;
using UnityEngine;

namespace ItemLogic
{
    [RequireComponent(typeof(Collider))]
    public class PickUpLogic : MonoBehaviour
    {
        [SerializeField] private float delayBeforePickUp = 5f; // Delay in seconds before picking up the item
        
        private Coroutine _pickUpCoroutine;
        private GameObject _thisItem;
        
        private static bool IsPlayer(GameObject obj)
        {
            return obj.CompareTag("Player");
        }
        
        // Use Awake for initialization
        private void Awake()
        {
            _thisItem = gameObject;
            EnsureColliderIsTrigger();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (IsPlayer(other.gameObject))
            {
                _pickUpCoroutine = StartCoroutine(PickUpAfterDelay(delayBeforePickUp));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_pickUpCoroutine != null && IsPlayer(other.gameObject))
            {
                StopCoroutine(_pickUpCoroutine);
                _pickUpCoroutine = null;
            }
        }
        
        /// <summary>
        /// Initiates a coroutine that waits for a specified delay before invoking the item pickup event.
        /// This method is designed to be called when a the controller of the player character enters the trigger area of the item,
        /// starting a countdown. If the player remains in the area for the duration of the delay, the item
        /// is considered picked up, and the corresponding event is triggered.
        /// </summary>
        /// <param name="delay">The time in seconds to wait before invoking the item picked up.</param>
        private IEnumerator PickUpAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            ItemPickUpObserver.OnItemPickUp.Invoke(_thisItem);
            Destroy(_thisItem);
        }
        
        // <summary>
        /// Ensures the attached Collider component is set as a trigger.
        /// This method is a safeguard and should be manually checked as well.
        /// </summary>
        private void EnsureColliderIsTrigger()
        {
            Collider colliderComponent = GetComponent<Collider>();
            if (colliderComponent != null)
            {
                colliderComponent.isTrigger = true;
            }
        }
    }
}