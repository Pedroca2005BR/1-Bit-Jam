using UnityEngine;

public class RectilinearMotionProjectile : ProjectileBaseBehaviour
{
    [SerializeField] private float _moveSpeed;

    void Update()
    {
        _rb.linearVelocity = _orientation * _moveSpeed;    
    }
}
