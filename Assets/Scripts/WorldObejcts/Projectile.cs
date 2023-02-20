using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float _damage;
    private Rigidbody2D _rigidbody2D;
    private Vector3 _mousePosition;
    private Vector3 _direction;
    
    public float Damage
    {
        get => _damage;
    }
    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _rigidbody2D.velocity = new Vector2(_direction.x, _direction.y).normalized * speed;
    }
}
