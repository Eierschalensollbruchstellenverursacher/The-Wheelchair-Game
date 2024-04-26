using System.Collections.Generic;
using FallingObstacles.InternalLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FallingObstacles
{
    public class CreateSpawnEventsOnChildren : MonoBehaviour
    {
        private CustomXRGrabInteractable grabInteractableScript;
        private FallingObstacleFactoryWrapper fallingObstacleFactoryWrapperScript;

        [SerializeField] private int minChildren = 1;
        [SerializeField] private int maxChildren = 10;
        [SerializeField] private List<FallingObstacleConfig> possibleObstacleConfigs = new List<FallingObstacleConfig>();

        private void Awake()
        {
            // Retrieve the necessary components from the GameObject this script is attached to
            grabInteractableScript = GetComponent<CustomXRGrabInteractable>();
            fallingObstacleFactoryWrapperScript = GetComponent<FallingObstacleFactoryWrapper>();
        }

        private void Start()
        {
            // Start the process of applying scripts to a random selection of child GameObjects
            ApplyScriptsToRandomChildren(minChildren, maxChildren);
            
            // Destroy the components from the parent GameObject
            DestroyComponentsFromParent();
        }

        // Applies scripts to a random number of children, determined by the input parameters `minChildren` and `maxChildren`
        private void ApplyScriptsToRandomChildren(int minChildren, int maxChildren)
        {
            // Obtain a shuffled list of indices representing the child GameObjects
            var childIndices = GetShuffledChildIndices();
            // Determine the number of children to apply scripts to, which is a random number between minChildren and maxChildren
            var numChildrenToApply = GetRandomNumberInRange(minChildren, maxChildren + 1);

            // Apply scripts to the randomly selected children based on the shuffled indices
            ApplyScriptsToChildren(childIndices, numChildrenToApply);
        }

        // Shuffles a list of integers representing indices using the Fisher-Yates shuffle algorithm
        private static void Shuffle(IList<int> list)
        {
            var randomNumberGen = new System.Random();
            var remainingItems = list.Count;
            while (remainingItems > 1)
            {
                remainingItems--;
                var randomIndex = randomNumberGen.Next(remainingItems + 1);
                // Swap the elements at the current index and the randomly chosen index
                (list[randomIndex], list[remainingItems]) = (list[remainingItems], list[randomIndex]);
            }
        }

        // Creates and returns a shuffled list of integers where each integer represents an index of a child GameObject
        private List<int> GetShuffledChildIndices()
        {
            var childIndices = new List<int>(transform.childCount);
            for (var i = 0; i < transform.childCount; i++) childIndices.Add(i);

            // Shuffle the list to randomize the order of indices
            Shuffle(childIndices);

            return childIndices;
        }

        // Returns a random integer within the specified range [min, max]
        private static int GetRandomNumberInRange(int min, int max)
        {
            return Random.Range(min, max);
        }

        // Applies scripts to children based on the provided list of indices and the number of children to apply to
        private void ApplyScriptsToChildren(IReadOnlyList<int> childIndices, int numberOfChildrenToApply)
        {
            for (var i = 0; i < numberOfChildrenToApply; i++)
            {
                // Access the child GameObject using the shuffled index
                var child = transform.GetChild(childIndices[i]);
                // Add necessary listeners to the child GameObject
                AddListenerToChild(child);
            }
        }

        // Adds a listener to the child GameObject to trigger events
        private void AddListenerToChild(Component child)
        {
            // Ensure the child has the required components and add a listener to trigger the spawning of obstacles
            var grabInteractable = child.GetComponent<CustomXRGrabInteractable>();
            var fallingObstacleFactoryWrapper = child.GetComponent<FallingObstacleFactoryWrapper>();

            // If the components are not found on the child, add them to the child GameObject
            if (grabInteractable == null)
            {
                grabInteractable = child.gameObject.AddComponent<CustomXRGrabInteractable>();
            }
            if (fallingObstacleFactoryWrapper == null)
            {
                fallingObstacleFactoryWrapper = child.gameObject.AddComponent<FallingObstacleFactoryWrapper>();
            }

            // Initialize the scripts on the child GameObject
            grabInteractable.Initialize(fallingObstacleFactoryWrapper);
            fallingObstacleFactoryWrapper.Initialize(grabInteractable);

            // Randomly select a FallingObstacleConfig from the possible configs
            if (possibleObstacleConfigs.Count > 0)
            {
                var selectedConfig = possibleObstacleConfigs[Random.Range(0, possibleObstacleConfigs.Count)];
                fallingObstacleFactoryWrapper._spawnedObstacleConfigs.Add(selectedConfig);
            }

            // Add the SpawnObstacle method to the SelectEntered event of the CustomXRGrabInteractable script
            grabInteractable.selectEntered.AddListener((interactor) => fallingObstacleFactoryWrapper.SpawnObstacle());
        }

        // Ensures that the specified component exists on the child GameObject, adding it if it does not
        private static T EnsureComponent<T>(Component child) where T : Component
        {
            var component = child.GetComponent<T>();
            if (component == null)
                // If the component is not present, add it to the GameObject
                component = child.gameObject.AddComponent<T>();
            return component;
        }
        private void DestroyComponentsFromParent()
        {
            // Destroy the components from the parent GameObject
            Destroy(grabInteractableScript);
            Destroy(fallingObstacleFactoryWrapperScript);
        }
    }
}