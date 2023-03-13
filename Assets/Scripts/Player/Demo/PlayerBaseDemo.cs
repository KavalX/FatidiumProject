using System;
using UnityEngine;

public class PlayerBaseDemo : MonoBehaviour
{
    [SerializeField] protected float speed;

    private SpriteRenderer _spriteRenderer;
    private GameObject _player;
    private GameObject _ghost;
    protected Rigidbody2D _rigidbody2D;
    
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    
    protected void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _ghost = GameObject.FindGameObjectWithTag("Ghost");
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        //movement inputs
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody2D.velocity = (Vector3.up * (speed ));
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody2D.velocity =(Vector3.down * (speed ));
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody2D.velocity =(Vector3.left * (speed ));
            _spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody2D.velocity =(Vector3.right * (speed ));
            _spriteRenderer.flipX = true;
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.W))
        {
            _rigidbody2D.velocity =(Vector3.zero);

        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rigidbody2D.velocity =(Vector3.up * (speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rigidbody2D.velocity =(Vector3.down * (speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody2D.velocity =(Vector3.left * (speed * Time.deltaTime));
            _spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody2D.velocity =(Vector3.right * (speed * Time.deltaTime));
            _spriteRenderer.flipX = true;
        }
    } 
}
