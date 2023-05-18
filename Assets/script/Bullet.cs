using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public const int StandardDamage = 5;
    public const int FullDamage = 20;
    Rigidbody2D rb;
    Vector2 direction;
    float speed = 1000f;
    public Player Shooter;
    
    public int Damage { get; set; }

    public GameObject standarddamage;
    public GameObject MaxDamage;
    
    

   // Start is called before the first frame update
    void Start()

        {
            rb = GetComponent<Rigidbody2D>();
            direction = transform.right;

           
        }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (direction * speed * Time.fixedDeltaTime);

    }
    
      void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {

            Shooter.IncreaseRage(10) ;
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null )
            {
                enemy.Life -= Damage;

              
            }
            
            Destroy(gameObject);
        }
       
    }


}