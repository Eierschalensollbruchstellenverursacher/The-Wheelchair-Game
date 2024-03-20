using UnityEngine;

public sealed class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    [field:SerializeField] public 

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Unable to create '{gameObject.name}'. Another instance of '{nameof(StatsManager)}' type exists on this scene.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
