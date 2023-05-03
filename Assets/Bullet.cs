using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public const int StandardDamage = 10;
    public const int FullDamage = 100;
    Rigidbody2D rb;
    Vector2 direction;
    float speed = 1000f;
    public Movement2D Shooter;
    public int Damage { get; set; }
    

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Shooter.Rage += 10;
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null )
            {
                enemy.Life -= Damage;

              if (enemy.Life <= 0)
                    Destroy(collision.gameObject);
            }
            
        }
        
    }


}