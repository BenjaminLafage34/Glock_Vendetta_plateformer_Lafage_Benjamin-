using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Life = 100;
    Animator animator;
    SpriteRenderer spriteRenderer;
    turretScript turretScript;
    public string Name;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        turretScript = GetComponentInParent<turretScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Life <= 0 && animator != null)
        {
            spriteRenderer.enabled = false;
            turretScript.enabled = false;
           
            animator.SetBool("TurretDead",true);
        }
    }
}
