using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class ShootProjectileComponent : MonoBehaviour
{
    [Header("Necessary References")]
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _attackPoint;

    [Header("Additional Information")]
    [SerializeField] private float _attackRange;
    [SerializeField] private float _shootCooldown;
    [SerializeField] private float _chargeTime;
    [SerializeField] private float _rechargeTime;

    [SerializeField] private bool _shootAtPlayer;   // Se true, projétil vai possuir informações em relação ao player.


    public bool _isDisabled {  get; private set; }
    private float _playtimeCooldown;
    private Transform _player;

    private void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>().transform;

        if (_projectile == null)
        {
            Debug.LogError("No projectile found!", this.gameObject);
            Destroy(this);
        }
    }

    private void Update()
    {
        // _isDisabled é usado para travar o resto do código enquanto algo está acontecendo numa coroutine
        if (_isDisabled)
        {
            return;
        }

        if (Vector2.Distance(_player.position, transform.position) > _attackRange)
        {
            return;
        }


        // Playtime cooldown é usado aqui como um timer decrescente que comanda quando o tiro deve ser feito
        _playtimeCooldown -= Time.deltaTime;

        if (_playtimeCooldown <= 0)
        {

            StartCoroutine(Shoot());
            _playtimeCooldown = _shootCooldown;
        }
    }

    private IEnumerator Shoot()
    {
        _isDisabled = true;

        // TO DO: Rodar animação aqui antes do yield return
        yield return new WaitForSeconds(_chargeTime);


        // Instantiate cria o projetil com base no prefab colocado em _projectile
        GameObject instance = Instantiate(_projectile, _attackPoint.position, Quaternion.identity);

        Vector2 orientation;

        // ShootAtPlayer é usado para identificar inimigos que atiram na direção do player. Isso também significa que eles olham sempre na direção do player
        if (_shootAtPlayer)
        {
            orientation = (_player.position - transform.position).normalized;

            if (orientation.x < 0)
            {
                Vector3 newScale = transform.localScale;
                newScale.x = -1;
                transform.localScale = newScale;
            }
            else
            {
                Vector3 newScale = transform.localScale;
                newScale.x = 1;
                transform.localScale = newScale;
            }
        }
        // Se shootAtPlayer for false, o tiro segue a orientação do objeto (inimigo) que atirou
        else
        {
            orientation = new Vector2(transform.localScale.x, transform.localScale.y);
        }

        // Necessário para que o projetil saiba para que lado ir
        instance.GetComponent<ProjectileBaseBehaviour>().SetDirection(orientation);

        // Necessário para acertar o posicionamento do ataque
        instance.transform.localScale = new Vector3(transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);

        yield return new WaitForSeconds(_rechargeTime);

        _isDisabled = false;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
