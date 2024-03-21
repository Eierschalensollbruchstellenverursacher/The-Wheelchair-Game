using System;
using UnityEngine;

public sealed class StatsManager : MonoBehaviour
{
    public Action OnScoreChange;
    public Action OnPlaytimeChange;

    [SerializeField] private float _currentScore;
    [SerializeField] private float _bestScore;

    [SerializeField] private float _playtimeSeconds;
    [SerializeField] private float _bestPlaytimeSeconds;

    public static StatsManager Instance { get; private set; }

    public float CurrentScore => _currentScore;
    public float BestScore => _bestScore;

    public float PlaytimeSeconds => _playtimeSeconds;
    public float BestPlaytimeSeconds => _bestPlaytimeSeconds;

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
