using System.Collections;
using UnityEngine;
public sealed class Enemy : MonoBehaviour
{ 
    [SerializeField] private float _defaultMovementCooldownSeconds = 3f;
    [SerializeField] private float _slowedMovementCooldownSeconds = 4f;
    [SerializeField] private float _spedUpMovementCooldownSeconds = 2f;
    [SerializeField] private float _unitsUp = 100f;
    [SerializeField] private bool _hasToMove = false, _isSpedUp = false, _isSlowed = false;
    private IEnumerator MoveOneUpCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _hasToMove = true;
    }
    private IEnumerator EnemySpeedUpCoroutine(float seconds)
    {
        _isSpedUp = true;
        yield return new WaitForSeconds(seconds);
        _isSpedUp = false;
    }
    private IEnumerator EnemySlowDownCoroutine(float seconds)
    {
        _isSlowed = true;
        yield return new WaitForSeconds(seconds);
        _isSlowed = false;
    }
    private float ÑurrentMovementCooldownSeconds
    { 
        get
        {
            if (_isSpedUp == true)  
                return _spedUpMovementCooldownSeconds;

            if ( _isSlowed == true) 
                return _slowedMovementCooldownSeconds;

            return _defaultMovementCooldownSeconds;
        }
    }
    public void SlowDown() 
    {
        if (_isSpedUp == true) 
            _isSpedUp = false;

        else StartCoroutine(EnemySlowDownCoroutine(_slowedMovementCooldownSeconds));
    }
    public void SpeedUp() 
    {
        if (_isSlowed == true) 
            _isSlowed = false;

        else StartCoroutine(EnemySpeedUpCoroutine(_spedUpMovementCooldownSeconds));
    }
    public void MoveOneUp()
    {
        transform.position += new Vector3(0, _unitsUp, 0) * Time.fixedDeltaTime;
    }
    private void Start()
    {
        StartCoroutine(MoveOneUpCoroutine(ÑurrentMovementCooldownSeconds));
    }
    private void FixedUpdate()
    {
        if (_hasToMove)
        {
            StartCoroutine(MoveOneUpCoroutine(ÑurrentMovementCooldownSeconds));
            MoveOneUp();
            _hasToMove = false;
        }
    }
}
