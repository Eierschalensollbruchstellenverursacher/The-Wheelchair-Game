using UnityEngine;
public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followDistance = 2f; //How close object should get to the target object before stopping.
    [SerializeField] private float followSpeed = 2f; //How fast the following object should move towards the target object horizontally.
    private Vector3 _targetPosition;

    public Transform Target => target;
    private void Update()
    {
        if (target == null) return;
        
        _targetPosition = target.transform.position;
        _targetPosition.y = transform.position.y;

        Vector3 targetDirection = (_targetPosition - transform.position).normalized;
        float targetDistance = Vector3.Distance(_targetPosition, transform.position);

        if (targetDistance > followDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, followSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_targetPosition.x, transform.position.y, transform.position.z), followSpeed * Time.deltaTime);
        }
    }

    private void HandleReachingTarget()
    {
        
        Debug.Log($"{nameof(HorizontalMovement)}: Monster collision");

    }
    private void HandleHittingTarget()
    {
        
        Debug.Log($"{nameof(HorizontalMovement)}: Laser collision");

    }
    private void OnEnable()
    {

        Enemy.OnReachingTarget += HandleReachingTarget;
        Enemy.OnHittingTarget += HandleHittingTarget;

    }
    public void OnDisable()
    {

        Enemy.OnHittingTarget -= HandleHittingTarget;
        Enemy.OnReachingTarget -= HandleReachingTarget;

    }
}