using UnityEngine;

public class PatrolComponent : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _moveSpeed;
    private int _nextPoint;

    [SerializeField] private float _marginOfError;

    void Start()
    {
        _nextPoint = 0;
    }

    void Update()
    {
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
            // Se não, ele olha pra direita
            else
            {
                Vector3 localScale = transform.localScale;
                localScale.x = 1;
                transform.localScale = localScale;
            }
        }

        // Faz o movimento para o proximo ponto
        transform.position = Vector2.MoveTowards(transform.position, _patrolPoints[_nextPoint].position, _moveSpeed * Time.deltaTime);
    }
}
