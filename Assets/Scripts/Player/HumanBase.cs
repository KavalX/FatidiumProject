using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBase : PlayerBase
{
    [SerializeField] protected float health;
    [SerializeField] protected float strength;
    [SerializeField] protected float size;
    
    public float Health
    {
        get => health;
        set => health = value;
    }
    
    protected new void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
        base.Update();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }
    }
}
