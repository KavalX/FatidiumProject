using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    Rigidbody2D _rigidbody2D;
    private Vector3 _mousePosition;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Awake()
    {
        //_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = (_mousePosition - transform.position).normalized;
        _rigidbody2D.velocity = (_mousePosition) * speed;
        
    }

    void Update()
    {
        
    }
}
