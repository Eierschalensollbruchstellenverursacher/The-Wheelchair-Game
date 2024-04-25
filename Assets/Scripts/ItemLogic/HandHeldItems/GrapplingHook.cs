using UnityEngine;

namespace ItemLogic.HandHeldItems
{
    public class GrapplingHook : MonoBehaviour
    {
        // Public method to destroy this game object by Unity Event
        public void SelfDestruct()
        {
            Destroy(this.gameObject);
        }
    }
}