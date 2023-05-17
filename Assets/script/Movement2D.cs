using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.GraphicsBuffer;

public class Movement2D : MonoBehaviour
{
    float horizontalValue;
    float verticalValue;
    Rigidbody2D rb;
    Vector3 m_Velocity = Vector3.zero;
    [SerializeField] private float m_MovementSmoothing = 1f;
    float speed = 40f;
    private float m_JumpForce = 1300f;
    private bool jumping = false;
    public GameObject pivotGun;
    public GameObject spawnPoint;
    public GameObject bulletRef;
    public GameObject StandardBullet;
    public GameObject Rage_bullet;


    public SpriteRenderer bras;
    public int JumpCount;
    private bool canDash = true;
    private float dashingPower = 18f;
    public bool isDashing;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 2f;
    public Animator animator;
    int bulletCount = 0;
    [SerializeField] private TrailRenderer tr;
    
   
    

    SpriteRenderer sr;

    public int Rage { get; internal set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Update()
    {
        /*
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");

        if (horizontalValue > 0) sr.flipX = false;
        else if (horizontalValue < 0) sr.flipX = true;
        */
        if (isDashing)
        {
            return;
        }
        if (rb.velocity.y == 0)

        {

            JumpCount = 0;


        }
      

        horizontalValue = Input.GetAxis("Horizontal");

        animator.SetFloat("speed", Mathf.Abs(horizontalValue));

   
        if (Input.GetButtonDown("Jump") && jumping == false && JumpCount < 2)
        {
            jumping = true;
           

        }
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 v_diff = (target - transform.position).normalized;
        float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);

        bool isJumping = (Math.Abs(rb.velocity.y) >= 0.01);
        animator.SetBool("IsJumping", isJumping);




        if (pivotGun.transform.rotation.eulerAngles.z > 90 && pivotGun.transform.rotation.eulerAngles.z < 270)
        {
           // bras.flipY = true;

            spawnPoint.GetComponent<Transform>().localPosition = new Vector2(1.9f, -0.35f);
        }
        else
        {
            //bras.flipY = false;

            spawnPoint.GetComponent<Transform>().localPosition = new Vector2(1.9f, 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            animator.SetBool("IsDashing", true);

            StartCoroutine(Dash());
            


        }
        
        //Debug.Log(v_diff.x);

        //sr.flipX = (v_diff.x < 0);
        if (v_diff.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            pivotGun.transform.rotation = Quaternion.Euler(0f, 0f, 180+atan2 * Mathf.Rad2Deg);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            pivotGun.transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg);
        }
        if (Input.GetButtonDown("Fire1"))
        {

            // 5eme balle plus puissante
            GameObject go;
            bulletCount++;
            if (bulletCount % 5 == 0)
                go = CreateBulletInstance(Rage_bullet, Bullet.FullDamage);
            else
                go = CreateBulletInstance(StandardBullet, Bullet.StandardDamage);






            //Instantiate(bulletRef, spawnPoint.transform.position, pivotGun.transform.rotation);
            
            go.transform.position = spawnPoint.transform.position;
            if (transform.localScale.x == -1)
            {
                go.transform.eulerAngles = pivotGun.transform.eulerAngles + new Vector3(0, 0, 180);

            } else
            {
                go.transform.eulerAngles = pivotGun.transform.eulerAngles;

            }


        }


    }
    private GameObject CreateBulletInstance(GameObject bulletType, int damage)
    {
        GameObject go = Instantiate(bulletType);
        Bullet b = go.GetComponent<Bullet>();
        b.Damage = damage;
        b.Shooter = GetComponent<Player>();
        return go;
    }
   
    
    
    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("IsDashing", false) ;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        

    }

    public void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Vector3 targetVelocity = new Vector2(horizontalValue * 10f * speed * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        if (jumping)
        {
            rb.AddForce(new Vector2(0f, m_JumpForce));
            JumpCount++;
            jumping = false;


        }
        
        
    }
   /* internal string RageAttack()
    {
        animator.SetBool("RageAttackEnable", true);

    }/*
     /*Vector3 AdjustVelocityToStope(Vector3 velocity)
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;
        
            if (adjustedVelocity.y < 0)
            {
                return adjustedVelocity;
            }
        }
        return velocity;
    }*/

}



