using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class M79Bullet : MonoBehaviour
{
    public bool Test;

    public Player Player;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        // Vérifie si le joueur entre en collision avec la zone de dégâts
        if (other.collider.tag == "Sol")
        {
           // GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            animator.enabled = true;
            Debug.Log("La balle touche le sol");
            //  animator.enabled = true;
        }
    }
   

    public void PrintBool()
    {
        
        var distance = (Player.transform.position - transform.position).magnitude;
        Debug.Log($"Distance Balle Joueur {distance}");
        if (distance < 5)
        {
            Player.TakeDamage(1);
            Destroy(this);
        }
    }


}
