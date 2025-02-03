using UnityEngine;

public class ShootProjectileComponent : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _shootCooldown;

    private float _playtimeCooldown;
    // TO DO: Player Reference

    private void Start()
    {
        if (_projectile == null)
        {
            Debug.LogError("No projectile found!", this.gameObject);
            Destroy(this);
        }
    }

    private void Update()
    {
        // TO DO: Calcular se o player está dentro do attack range. Se não, retorna.

        _playtimeCooldown -= Time.deltaTime;

        if (_playtimeCooldown <= 0)
        {
            Shoot();
            _playtimeCooldown = _shootCooldown;
        }
    }

    private void Shoot()
    {
        GameObject instance = Instantiate(_projectile, transform.position, Quaternion.identity, transform);

        Vector2 orientation = new Vector2(transform.localScale.x, transform.localScale.y);
        instance.GetComponent<ProjectileBaseBehaviour>().SetDirection(orientation);
    }
}
