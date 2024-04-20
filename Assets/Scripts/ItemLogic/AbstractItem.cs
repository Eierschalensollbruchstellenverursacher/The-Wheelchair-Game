using UnityEngine;

namespace ItemLogic
{
    public abstract class AbstractItem : MonoBehaviour
    {
        protected float UseDuration;
        public abstract void Use();
    }
}
