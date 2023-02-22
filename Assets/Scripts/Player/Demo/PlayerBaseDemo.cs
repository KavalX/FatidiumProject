using System;
using UnityEngine;

public class PlayerBaseDemo : PlayerControl
{
    [SerializeField] protected float speed;

    private SpriteRenderer _spriteRenderer;
    private GameObject _player;
    private GameObject _ghost;

    protected void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _ghost = GameObject.FindGameObjectWithTag("Ghost");
    }

    protected void Update()
    {
        //movement inputs
        if (Input.GetKey(KeyCode.W))
        {
            _player.transform.Translate(Vector3.up * (speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            _player.transform.Translate(Vector3.down * (speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            _player.transform.Translate(Vector3.left * (speed * Time.deltaTime));
            _spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _player.transform.Translate(Vector3.right * (speed * Time.deltaTime));
            _spriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _ghost.transform.Translate(Vector3.up * (speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _ghost.transform.Translate(Vector3.down * (speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _ghost.transform.Translate(Vector3.left * (speed * Time.deltaTime));
            _spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _ghost.transform.Translate(Vector3.right * (speed * Time.deltaTime));
            _spriteRenderer.flipX = true;
        }
    } 
}
