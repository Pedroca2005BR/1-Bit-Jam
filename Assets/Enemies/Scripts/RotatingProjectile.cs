using UnityEngine;

public class RotatingProjectile : ProjectileBaseBehaviour
{
    [SerializeField] private Transform _childToRotate;
    [SerializeField] private float _rotationSpeed;

    void Update()
    {
        // Rodando conferindo o localScale é importante para sempre rotacionar para cima
        if (transform.localScale.x > 0)
        {
            _childToRotate.RotateAround(transform.position, Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            _childToRotate.RotateAround(transform.position, Vector3.back, _rotationSpeed * Time.deltaTime);
        }
    }
}
