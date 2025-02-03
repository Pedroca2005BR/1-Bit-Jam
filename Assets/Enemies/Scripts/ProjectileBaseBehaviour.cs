using UnityEngine;

public class ProjectileBaseBehaviour : MonoBehaviour
{
    [SerializeField] protected ProjectileDirection _direction;
    protected Vector2 _orientation = Vector2.zero;
    protected Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        if (_direction == ProjectileDirection.Horizontal) _orientation = new Vector2(direction.x, 0);
        else if (_direction == ProjectileDirection.Vertical) _orientation = new Vector2(0, direction.y);
        else if (_direction == ProjectileDirection.Diagonal) _orientation = new Vector2(direction.x, direction.y);
    }

}

public enum ProjectileDirection
{
    None,
    Horizontal,
    Vertical,
    Diagonal
}