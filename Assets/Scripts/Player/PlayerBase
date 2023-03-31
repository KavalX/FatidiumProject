using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBase : PlayerControl
{
    [SerializeField] protected float speed;

    private SpriteRenderer _spriteRenderer;

    protected void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        //movement inputs
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * (speed ));
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * (speed ));
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * (speed ));
            _spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * (speed ));
            _spriteRenderer.flipX = true;
        } 
    } 
    public void spawnPlayer(PlayerType type)
    {
        if (type == PlayerType.human)
        {
            this.AddComponent<Jhon>();

        }
    }
}

public enum PlayerType
{
    human,
    Gosht
}