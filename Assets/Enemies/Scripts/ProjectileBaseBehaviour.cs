using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using static UnityEditor.Experimental.GraphView.GraphView;
#endif

[RequireComponent (typeof(Rigidbody2D))]
public class ProjectileBaseBehaviour : MonoBehaviour
{
    public ProjectileDirection _direction;  // A dire��o que o projetil vai seguir.
    [SerializeField] protected float _lifeSpan = 5; // O tempo de vida que o projetil pode ter no m�ximo

    protected Vector2 _orientation = Vector2.zero;
    protected Rigidbody2D _rb;
    protected Transform _player;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindFirstObjectByType<PlayerMovement>().transform;
        StartCoroutine(SelfDestruct(_lifeSpan));
    }

    // SetDirection � usado para controlar em qual dire��o o objeto vai se movimentar. Homing Projectile n�o necessita de dire��o.
    public void SetDirection(Vector2 direction)
    {
        if (_direction == ProjectileDirection.Horizontal) _orientation = new Vector2(direction.x, 0);
        else if (_direction == ProjectileDirection.Vertical) _orientation = new Vector2(0, direction.y);
        else if (_direction == ProjectileDirection.Diagonal) _orientation = new Vector2(direction.x, direction.y);
    }


    // SelfDestruct � usado para destruir o projetil depois de um certo tempo.
    IEnumerator SelfDestruct(float time)
    {
        //Debug.Log("Starting Life CountDown!");
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}

public enum ProjectileDirection
{
    None,
    Horizontal,
    Vertical,
    Diagonal,
    Homing
}