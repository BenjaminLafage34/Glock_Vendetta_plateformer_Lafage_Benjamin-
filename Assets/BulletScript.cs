using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            Player p = Col.gameObject.GetComponent<Player>();
            p.TakeDamage(10);
            Destroy(this.gameObject);
        }
    }
}
