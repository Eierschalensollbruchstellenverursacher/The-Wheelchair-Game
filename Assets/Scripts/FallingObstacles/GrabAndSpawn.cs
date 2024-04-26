using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace FallingObstacles
{
    public class GrabAndSpawn : MonoBehaviour
    {
        private XRGrabInteractable _grabInteractable;
        private FallingObstacleFactoryWrapper _fallingObstacleFactoryWrapper;

        private void Awake()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();
            _fallingObstacleFactoryWrapper = GetComponent<FallingObstacleFactoryWrapper>();
        }

        private void OnEnable()
        {
            _grabInteractable.selectEntered.AddListener(HandleSelectEntered);
        }

        private void OnDisable()
        {
            _grabInteractable.selectEntered.RemoveListener(HandleSelectEntered);
        }

        private void HandleSelectEntered(SelectEnterEventArgs args)
        {
            _fallingObstacleFactoryWrapper.SpawnObstacle();
        }
    }
}