using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(HorizontalMovement))] 
public sealed class Enemy : MonoBehaviour
{ 
    public static Action OnReachingTarget;
    public static Action OnHittingTarget;

    [Header("Movement Cooldowns")]
    [SerializeField] private float _defaultMovementCooldownSeconds = 3f;
    [SerializeField] private float _slowedMovementCooldownSeconds = 4f;
    [SerializeField] private float _spedUpMovementCooldownSeconds = 2f;
    
    private float _currentAttackCooldownSeconds = 0f;

    [Space]

    [Header("Attack Options")]
    [SerializeField] private int _minAttackCooldownSeconds = 5;
    [SerializeField] private int _maxAttackCooldownSeconds = 20;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private Vector3 _laserOffset;

    [Space]

    [Header("Movement")]
    [SerializeField] Vector3 startingPosition;
    [SerializeField] private float _unitsUp = 100f;
    private Transform _target;

    [Space]

    [Header("Booleans")]
    [SerializeField] private bool _hasToMove = false;
    [SerializeField] private bool _isSpedUp = false;
    [SerializeField] private bool _isSlowed = false;
    [SerializeField] private bool _resetPosition = false;
    [SerializeField] private bool _canMoveUp = true; // New variable to prevent enemy from moving

    [Space]
    [Header("Sounds")]
    [SerializeField] private AudioClip _attackSoundQ;

    private AudioSource _audioSource;



    private float currentMovementCooldownSeconds
    { 
        get
        {
            if (_isSpedUp == true)  
                return _spedUpMovementCooldownSeconds;

            if (_isSlowed == true) 
                return _slowedMovementCooldownSeconds;

            return _defaultMovementCooldownSeconds;
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _target = GetComponent<HorizontalMovement>().Target;

        StartCoroutine(MoveOneUpCoroutine(currentMovementCooldownSeconds));
        GenerateCooldown();

        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (_resetPosition)
        {
            ResetPosition();
            _resetPosition = false;
        }
        
        _currentAttackCooldownSeconds -= Time.deltaTime;

        if (_currentAttackCooldownSeconds <= 0)
        {
            AttackWithLaser();
            GenerateCooldown();
        }    
    }

    [ContextMenu("Attack")]
    public void AttackWithLaser()
    {

        _audioSource.PlayOneShot(_attackSoundQ, 0.1f);

        Vector3 currentEnemyPosition = gameObject.transform.position;

        Vector3 laserSpawnPosition = new Vector3
        (
            currentEnemyPosition.x + _laserOffset.x,
            currentEnemyPosition.y + _laserOffset.y,
            currentEnemyPosition.z + _laserOffset.z
        );

        GameObject laser = Instantiate(_laserPrefab, laserSpawnPosition, Quaternion.identity, this.gameObject.transform);
    }

    public void GenerateCooldown()
    {
        _currentAttackCooldownSeconds = Random.Range(_minAttackCooldownSeconds, _maxAttackCooldownSeconds);
    }

    public void ResetPosition()
    {
        transform.position = startingPosition;
    }

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
        if (_canMoveUp)
        {
            transform.position += new Vector3(0, _unitsUp, 0) * Time.fixedDeltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (_hasToMove && _canMoveUp)
        {
            StartCoroutine(MoveOneUpCoroutine(currentMovementCooldownSeconds));
            MoveOneUp();
            _hasToMove = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == _target)
        {
            OnReachingTarget?.Invoke();
        }
    }
}