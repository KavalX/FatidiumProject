using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBase : PlayerBase
{
    [SerializeField] protected float health;
    [SerializeField] protected float strength;
    [SerializeField] protected float size;
    [SerializeField] protected bool hasKey = false;
    [SerializeField] protected float sprintLimit = 3f;
    
    public float Health
    {
        get => health;
        set => health = value;
    }
    
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door") && hasKey)
        {
            Debug.Log("Puerta abierta");
            collision.gameObject.GetComponent<Door>().OpenDoor();
            hasKey = false;
        }

        if (collision.gameObject.CompareTag("Key"))
        {
            Debug.Log("Llave recogida");
            hasKey = true;
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Golpeado");
            health -= collision.gameObject.GetComponent<Projectile>().Damage;
            Destroy(collision.gameObject);
        }
    } 

    protected new void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
        base.Update();
        
        if (Input.GetKey(KeyCode.LeftShift) && sprintLimit > 0)
        {
            speed = 10f;
            sprintLimit -= Time.deltaTime;
        }
        else if (sprintLimit < 3f)
        {
            speed = 5f;
            sprintLimit += Time.deltaTime;
        }
    }
}
