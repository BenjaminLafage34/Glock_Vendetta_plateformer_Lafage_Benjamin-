using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarolinaIA : MonoBehaviour
{

    public Movement2D Player;

    public GameObject BalleQuiTombe;
    // Start is called before the first frame update
    void Start()
    {

    }

    DateTime LastShoot = DateTime.Now;

    // Update is called once per frame
    void Update()
    {
        DateTime test = DateTime.Now;

        if ((DateTime.Now - LastShoot).TotalSeconds < 5)
            return;

        LastShoot = DateTime.Now;
        BalleQuiTombe.transform.position = new Vector3(Player.transform.position.x, 40);
        BalleQuiTombe.GetComponent<M79Bullet>().Player = Player.GetComponent<Player>();
        Rigidbody2D rigidBody =  BalleQuiTombe.GetComponent<Rigidbody2D>();
        BalleQuiTombe.GetComponent<Rigidbody2D>().velocity  = new Vector2(2,-50);
        Instantiate(BalleQuiTombe);

    }
}
