using UnityEngine;

public class FollowPlayerComponent : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _followRadius;

    private Transform _player;
    private Rigidbody2D _rb;

    private void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>().transform;
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (Vector2.Distance(_player.position, transform.position) < _followRadius)
        {
            Follow();
        }
    }

    private void Follow()
    {
        Vector2 direction = (_player.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(transform.up, direction);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        _rb.SetRotation(rotation);

        _rb.linearVelocity = transform.forward * _followSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _followRadius);
    }
}
