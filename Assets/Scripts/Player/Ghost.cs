using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : PlayerBase
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cooldown;
    [SerializeField] private float projectileSpeed;
    private float _actualCooldown;
    private Vector3 _mousePosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private new void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_actualCooldown <= 0)
            {
                GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);
                projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(_mousePosition.x * projectileSpeed, _mousePosition.y*projectileSpeed);
                _actualCooldown = cooldown;
            }
        }
        else
        {
            _actualCooldown -= Time.deltaTime;
        }
    }
}
