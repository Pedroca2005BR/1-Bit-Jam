using UnityEngine;

public class ForceBasedProjectile : ProjectileBaseBehaviour
{
    // Floor � o m�nimo de for�a e Ceiling � o m�ximo
    [SerializeField] private float _forceMultiplierFloor;
    [SerializeField] private float _forceMultiplierCeiling;


    private void Update()
    {
        if (_orientation != Vector2.zero)
        {
            _orientation.x *= 0.5f;
            _rb.AddForce(_orientation * Random.Range(_forceMultiplierFloor, _forceMultiplierCeiling));
            Destroy(this);
        }
    }

    
}
