using UnityEngine;

[RequireComponent(typeof(ShootProjectileComponent))]
public class PatrolComponent : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _moveSpeed;
    private int _nextPoint;

    [SerializeField] private float _marginOfError;

    private ShootProjectileComponent _shootRef;

    void Start()
    {
        _shootRef = GetComponent<ShootProjectileComponent>();
        _nextPoint = 0;
    }

    void Update()
    {
        // Se o componente que atira desabilitou o update, o componente de patrulha é desabilitado temporariamente também.
        if (_shootRef._isDisabled) return;
        
        // Controla quando o próximo ponto foi alcançado e muda seu destino para o ponto seguinte
        if (Vector2.Distance(transform.position, _patrolPoints[_nextPoint].position) <= _marginOfError)
        {
            _nextPoint++;
            if (_nextPoint >= _patrolPoints.Length)  _nextPoint = 0;

            // Se o proximo ponto estiver à esquerda da posição atual, fazemos o inimigo olhar para esquerda
            if (_patrolPoints[_nextPoint].position.x < transform.position.x)
            {
                Vector3 localScale = transform.localScale;
                localScale.x = -1;
                transform.localScale = localScale;
            }
            // Se estiver a direita, ele olha pra direita
            else if (_patrolPoints[_nextPoint].position.x > transform.position.x)
            {
                Vector3 localScale = transform.localScale;
                localScale.x = 1;
                transform.localScale = localScale;
            }
            // DISCLAIMER: O else if foi implementado para não haver rotações por patrulha em caso de pontos de patrulha com o mesmo X
        }

        // Faz o movimento para o proximo ponto
        transform.position = Vector2.MoveTowards(transform.position, _patrolPoints[_nextPoint].position, _moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        
        for(int i=0; i<_patrolPoints.Length - 1; i++)
        {
            Gizmos.DrawWireSphere(_patrolPoints[i].position, 0.5f);
            Gizmos.DrawLine(_patrolPoints[i].position, _patrolPoints[i + 1].position);
        }

        Gizmos.DrawWireSphere(_patrolPoints[_patrolPoints.Length - 1].position, 0.5f);
        Gizmos.DrawLine(_patrolPoints[_patrolPoints.Length-1].position, _patrolPoints[0].position);

    }
}
