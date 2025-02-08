using UnityEngine;
using System.Collections;

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

    // Animation variables
    private Animator animator;
    private string currentAnimation = "";

    private void Start()
    {
        _shootRef = GetComponent<ShootProjectileComponent>();
        _player = FindFirstObjectByType<PlayerMovement>().transform;
        _rb = GetComponent<Rigidbody2D>();


        animator = GetComponent<Animator>(); // Pega referência do animator
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


        CheckAnimation(); //chaca animações
    }

    private void CheckAnimation()
    {
        if(_rb.linearVelocity.x < -0.1f || _rb.linearVelocity.x > 0.1f)
        {
            ChangeAnimation("walk_ini2");
            
           
        }
        else
        {

            ChangeAnimation("idle_ini2");
        }
    }

    public void ChangeAnimation(string animation, float crosfade = 0.2f, float time = 0)
    {
        if (time > 0) StartCoroutine(Wait());
        else Validate();

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(time - crosfade);


            Validate();
        }


        void Validate()
        {
            if (currentAnimation != animation)
            {
                currentAnimation = animation;
                if (currentAnimation == "")
                    CheckAnimation();
                else
                    animator.CrossFade(animation, crosfade);
            }
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
