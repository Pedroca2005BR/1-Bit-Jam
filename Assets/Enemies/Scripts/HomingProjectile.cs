using UnityEngine;

public class HomingProjectile : ProjectileBaseBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _followSpeed;


    private void Update()
    {
        Follow();
    }

    // Follow aqui controla a rotação e movimento do projetil em direção ao player
    private void Follow()
    {
        Vector2 direction = (_player.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(transform.up, direction);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        _rb.SetRotation(rotation);

        _rb.linearVelocity = _followSpeed * Time.deltaTime * transform.up;
    }
}
