using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public string Bullets = "Bullets";

    void Start()
    {

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(Bullets), true);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player p = col.gameObject.GetComponent<Player>();
            p.TakeDamage(3);
        }
        else if (col.gameObject.CompareTag("Sol"))
        {
            Destroy(gameObject);
        }
       
    }
}  
    

