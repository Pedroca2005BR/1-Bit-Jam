using UnityEngine;

public class ForceBasedProjectile : ProjectileBaseBehaviour
{
    [SerializeField] private float _forceMultiplierFloor;
    [SerializeField] private float _forceMultiplierCeiling;
    private void Update()
    {
        if (_orientation != Vector2.zero)
        {
            _rb.AddForce(_orientation * Random.Range(_forceMultiplierFloor, _forceMultiplierCeiling));
            Destroy(this);
        }
    }
}
