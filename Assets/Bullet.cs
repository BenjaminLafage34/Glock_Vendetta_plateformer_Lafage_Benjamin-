using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 direction;
    float speed = 1000f;

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
        if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    public void Test()
    {
        int a = 10;
        int res = NewMethod();
        a = res + a;

    }

    private static int NewMethod()
    {
        int res = 15;
        if (DateTime.Now.Date.Year % 2 == 0)
        {
            res = (int)(15 * Math.Cos(15));
            res = 15 + 100;
        }

        return res;
    }
}
