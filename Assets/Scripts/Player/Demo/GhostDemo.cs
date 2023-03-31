using System;
using UnityEngine;

public class GhostDemo : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cooldown;
    private float _actualCooldown;
    [SerializeField] private AudioSource _shootSound;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] protected float speed;
    [SerializeField] private VariableJoystick variableJoystick;
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private new void Update()
    {
        // Limitar velocidad diagonal
        if (_rigidbody.velocity.magnitude > speed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * speed;
        }

        _direction = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical);
        _rigidbody.velocity = _direction * speed;

        if (Input.GetKey(KeyCode.A) || _direction.x < 0)
        {
            _spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.D) || _direction.x > 0)
        {
            _spriteRenderer.flipX = true;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (_actualCooldown <= 0)
                    {
                        Instantiate(projectile, transform.position, Quaternion.identity);
                        _actualCooldown = cooldown;
                        _shootSound.Play();
                    }

                    else if (_actualCooldown > 0)
                    {
                        _actualCooldown -= Time.deltaTime;
                    }
                    break;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_actualCooldown <= 0)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    _actualCooldown = cooldown;
                    _shootSound.Play();
                }
            }
            else if (_actualCooldown > 0)
            {
                _actualCooldown -= Time.deltaTime;
            }
        }
    }
}