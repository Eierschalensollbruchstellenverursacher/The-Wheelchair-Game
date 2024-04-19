using System;
using System.Collections;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public enum States { None, Spawning, NotDamaging, Damaging, Despawning };
    private States _state;
    [SerializeField] private float _beamDiameter = 1f;
    [SerializeField] private float _noDamageTimeSeconds = 1f;
    [SerializeField] private float _damagingTimeSeconds = 2f;
    [SerializeField] private float _spawnWideningTimeSeconds = 0.5f;
    [SerializeField] private float _despawnNarrowingTimeSeconds = 0.05f;

    private float _currentDiameter;
    private Transform _target;

    private void Awake()
    {
        _target = GetComponentInParent<HorizontalMovement>().Target;

        _currentDiameter = 0f;
        
        StartCoroutine(SpawnBeam());
    }

    private IEnumerator SpawnBeam()
    {
        _state = States.Spawning;
        while (_currentDiameter < _beamDiameter)
        {
            _currentDiameter += Time.deltaTime / _spawnWideningTimeSeconds;
            transform.localScale = new Vector3(_currentDiameter, transform.localScale.y, _currentDiameter);
            yield return null;
        }
        StartCoroutine(StartDamagingAfterTimeout());
    }

    private IEnumerator StartDamagingAfterTimeout()
    {
        _state = States.NotDamaging;
        yield return new WaitForSeconds(_noDamageTimeSeconds);
        _state = States.Damaging;
        yield return new WaitForSeconds(_damagingTimeSeconds);
        StartCoroutine(DespawnBeam());
    }

    private IEnumerator DespawnBeam()
    {
        _state = States.Despawning;
        while (_currentDiameter > 0)
        {
            _currentDiameter -= Time.deltaTime / _despawnNarrowingTimeSeconds;
            transform.localScale = new Vector3(_currentDiameter, transform.localScale.y, _currentDiameter);
            yield return null;
        }
        Destroy(gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (_state == States.Damaging && other.gameObject.transform == _target)
        {
            Enemy.OnHittingTarget?.Invoke();
        }
    }
}
