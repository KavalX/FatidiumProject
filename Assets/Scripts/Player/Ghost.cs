using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : PlayerBase
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float cooldown;
    private Vector3 _mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;
    }

    // Update is called once per frame
    private new void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cooldown <= 0)
            {
                GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);
                
                projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(_mousePosition.x, _mousePosition.y, 0);
                cooldown = 5;
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
}
