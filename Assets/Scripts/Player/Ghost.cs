using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : PlayerBase
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cooldown;
    private float _actualCooldown;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_actualCooldown <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                _actualCooldown = cooldown;
            }
        }
        else if (_actualCooldown > 0)
        {
            _actualCooldown -= Time.deltaTime;
        }
    }
}
