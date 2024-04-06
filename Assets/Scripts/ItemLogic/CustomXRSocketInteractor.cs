using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ItemLogic
{
    /// <summary>
    /// Custom XR Socket Interactor that allows for dynamic instantiation and attachment of predefined GameObjects as children of this socket.
    /// This class supports attaching specific prefabs, such as a grappling hook or an umbrella, based on the tag of the incoming GameObject.
    /// </summary>
    public class CustomXRSocketInteractor : XRSocketInteractor
    {
        /// <summary>
        /// Instantiates a predefined prefab based on the tag of the incoming `item` and attaches it as a child to this socket.
        /// If the instantiated prefab has an XRBaseInteractable component, it is automatically selected.
        /// Note: The `item` GameObject's tag is used to determine which prefab to instantiate. For instance some supported tags are "GrapplingHook" and "Umbrella".
        /// </summary>
        /// <param name="item">The GameObject whose tag is used to determine which prefab to instantiate and attach. The original `item` is not directly attached.</param>
        public bool IsOccupied { get; private set; }

        [SerializeField] 
        private GameObject grapplingHookPrefab;
        [SerializeField] 
        private GameObject umbrellaPrefab;
        
        public virtual void AdjustAndAttachItem(GameObject item)
        {
            // Determine the correct prefab based on the item's information (e.g., tag)
            GameObject prefabToInstantiate = item.tag switch
            {
                "GrapplingHook" => grapplingHookPrefab,
                "Umbrella" => umbrellaPrefab,
                _ => null
            };
            if (prefabToInstantiate == null) return;
            
            // Instantiate the prefab and parent it to the socket
            GameObject instantiatedItem = Instantiate(prefabToInstantiate, transform);
            // If the instantiated item has an XRBaseInteractable component, select it
            if (instantiatedItem.TryGetComponent(out XRBaseInteractable interactable))
            {
                interactionManager.SelectEnter(this as IXRSelectInteractor, interactable as IXRSelectInteractable);
            }
            // Set IsOccupied to true when an item is attached
            IsOccupied = true;
        }
    }
}