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

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private new void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up * (speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.down * (speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * (speed * Time.deltaTime));
            _spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * (speed * Time.deltaTime));
            _spriteRenderer.flipX = true;
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
