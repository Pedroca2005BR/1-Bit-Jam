using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(ShootProjectileComponent))]
public class FollowPlayerComponent : MonoBehaviour
{
    [SerializeField] private Transform _restPoint;
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _followRadius;

    private Transform _player;
    private Rigidbody2D _rb;
    private ShootProjectileComponent _shootRef;

    private void Start()
    {
        _shootRef = GetComponent<ShootProjectileComponent>();
        _player = FindFirstObjectByType<PlayerMovement>().transform;
        _rb = GetComponent<Rigidbody2D>();

    }


    private void Update()
    {
        // Se o componente que atira desabilitou o update, o componente de perseguição é desabilitado temporariamente também.
        if (_shootRef._isDisabled)
        {
            return;
        }
        
        // Se o player está dentro do follow radius, começa a perseguição
        if (Vector2.Distance(_player.position, _rb.position) < _followRadius)
        {
            GoToTarget(_player);
        }
        else
        {

            if (Vector2.Distance(_restPoint.position, _rb.position) < 2f) return;
            GoToTarget(_restPoint);
            
        }
        
    }


    private void GoToTarget(Transform target)
    {
        Vector2 orientation = (target.position - transform.position).normalized;

        // Controla a direção para qual o objeto está olhando, de forma que sempre olha para seu objetivo: o player
        if (orientation.x < 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -1;
            transform.localScale = localScale;
        }
        else
        {
            Vector3 localScale = transform.localScale;
            localScale.x = 1;
            transform.localScale = localScale;
        }

        // Faz a movimentação apenas no eixo X, para que o inimigo não fique voando.
        _rb.linearVelocity = new Vector2(orientation.x, 0) * _followSpeed;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _followRadius);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_restPoint.position, 2f);
    }
}
