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

    public bool IsRageBullet = false;

    Rigidbody2D rb;
    Vector2 direction;
    /// <summary>
    /// Speed correspond au nombre de pixels parcourus par secondes de jeux.
    /// </summary>
    float speed = 1000f;
    public Player Shooter;
    private float TravelledDistance = 0;
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
        TravelledDistance += speed * Time.fixedDeltaTime;
        if (TravelledDistance > 1500)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (!IsRageBullet)
                Shooter.IncreaseRage(5);
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.AddDamages(Damage);
            }
            Destroy(gameObject);
            
        }
        if (collision.tag == "Sol")
        {
            Destroy(gameObject);
        }
    }
    
}