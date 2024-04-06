using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ItemLogic
{
    public class ItemPickUpEvent : UnityEvent<GameObject> { }
    public class ItemPickUpObserver : MonoBehaviour
    {
        /// <summary>
        /// Static event of type <see cref="ItemPickUpEvent"/> is triggered when an item is picked up.
        /// This event allows other game objects to subscribe and react to item pickup actions within the game.
        /// </summary>
        public static readonly ItemPickUpEvent OnItemPickUp = new ItemPickUpEvent();

        [SerializeField]
        private CustomXRSocketInteractor rightHandSocketInteractor;

        [SerializeField]
        private CustomXRSocketInteractor leftHandSocketInteractor;

        private readonly Queue<CustomXRSocketInteractor> _socketQueue = new Queue<CustomXRSocketInteractor>();

        private void Start()
        {
            _socketQueue.Enqueue(rightHandSocketInteractor);
            _socketQueue.Enqueue(leftHandSocketInteractor);
        }

        private void OnEnable()
        {
            OnItemPickUp.AddListener(AttachItemToNextSocket);
        }

        private void OnDisable()
        {
            OnItemPickUp.RemoveListener(AttachItemToNextSocket);
        }

        private void AttachItemToNextSocket(GameObject item)
        {
            int initialQueueCount = _socketQueue.Count;

            for (int i = 0; i < initialQueueCount; i++)
            {
                var socket = _socketQueue.Dequeue();
                if (!socket.IsOccupied)
                {
                    socket.AdjustAndAttachItem(item);
                    _socketQueue.Enqueue(socket);
                    return; // Item attached, exit the method
                }
                // If the socket is occupied, just enqueue it back and try the next one
                _socketQueue.Enqueue(socket);
            }
        }
    }
}