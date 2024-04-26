using FallingObstacles;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRGrabInteractable : XRGrabInteractable
{
    private FallingObstacleFactoryWrapper fallingObstacleFactoryWrapper;

    public void Initialize(FallingObstacleFactoryWrapper fallingObstacleFactoryWrapper)
    {
        // Set up the reference to the FallingObstacleFactoryWrapper script
        this.fallingObstacleFactoryWrapper = fallingObstacleFactoryWrapper;
    }
}