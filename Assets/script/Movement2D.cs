using System;
using System.Collections;
using UnityEngine;


enum LookingDirection
{
    Left, Right
}

public class Movement2D : MonoBehaviour
{
    float horizontalValue;
    float verticalValue;
    Rigidbody2D rb;
    Vector3 m_Velocity = Vector3.zero;
    [SerializeField] private float m_MovementSmoothing = 1f;
    float speed = 50f;
    private float m_JumpForce = 1400f;
    private bool jumping = false;
    private bool hasJumped = false;
    public GameObject pivotGun;
    public GameObject spawnPoint;
    public GameObject bulletRef;
    public GameObject StandardBullet;
    public GameObject Rage_bullet;
    private LookingDirection LookingDirection;

    public SpriteRenderer bras;
    public int JumpCount;
    private bool canDash = true;
    private float dashingPower = 20f;
    public bool isDashing;
    private bool isRaging = false;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
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

        /* Vector3 velocity = rb.velocity.y;
         velocity = AdjustVelocityToStope(velocity);*/

        if (Input.GetButtonDown("Jump") && jumping == false && JumpCount < 2)
        {
            jumping = true;
            hasJumped = true;
            animator.SetBool("IsJumping", true); // Déclenche l'animation de saut
        }
        else
        {
            bool isJumping = (Math.Abs(rb.velocity.y) >= 0.0001);
            if (!jumping && hasJumped && !isJumping)
            {
                hasJumped = false;
                animator.SetBool("IsJumping", false); // Arrête l'animation de saut
            }
        }

        float spawnPointYOffset = (isRaging ? -1.20f : 0.35f);
        if (pivotGun.transform.rotation.eulerAngles.z > 90 && pivotGun.transform.rotation.eulerAngles.z < 270)
        {
            // bras.flipY = true;
            spawnPoint.GetComponent<Transform>().localPosition = new Vector2(1.9f, -spawnPointYOffset);
        }
        else
        {
            //bras.flipY = false;
            spawnPoint.GetComponent<Transform>().localPosition = new Vector2(1.9f, spawnPointYOffset);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            animator.SetBool("IsDashing", true);
            StartCoroutine(Dash());
        }

        //Debug.Log(v_diff.x);
        //sr.flipX = (v_diff.x < 0);

        Vector3 v_diff, target;
        if (isRaging)
        {
            int sens = (LookingDirection.Left == LookingDirection ? -1 : 1);
            target = new Vector3(transform.position.x + sens, transform.position.y, transform.position.z);
            v_diff = (target - transform.position).normalized;
        }
        else
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v_diff = (target - transform.position).normalized;
        }
        float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);

        if (v_diff.x < 0)
        {
            LookingDirection = LookingDirection.Left;
            transform.localScale = new Vector2(-1, 1);
            pivotGun.transform.rotation = Quaternion.Euler(0f, 0f, 180 + atan2 * Mathf.Rad2Deg);
        }
        else
        {
            LookingDirection = LookingDirection.Right;
            transform.localScale = new Vector2(1, 1);
            pivotGun.transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            // 5eme balle plus puissante
            if (bulletCount % 5 == 0)
                CreateBulletInstance(Rage_bullet, Bullet.FullDamage);
            else
                CreateBulletInstance(StandardBullet, Bullet.StandardDamage);

            //Instantiate(bulletRef, spawnPoint.transform.position, pivotGun.transform.rotation);
        }
    }

    private void CreateBulletInstance(GameObject bulletType, int damage,bool IsRageBullet = false)
    {
        GameObject go = Instantiate(bulletType);
        Bullet b = go.GetComponent<Bullet>();
        b.IsRageBullet = IsRageBullet;
        b.Damage = damage;
        b.Shooter = GetComponent<Player>();
        bulletCount++;

        if (isRaging)
        {
            go.transform.position = spawnPoint.transform.position;
            if (bulletCount % 2 == 0)
            {
                go.transform.Translate(0, (LookingDirection == LookingDirection.Left ? -1 : 1) * 0.35f, 0);
            }
        }
        else
            go.transform.position = spawnPoint.transform.position;

        if (transform.localScale.x == -1)
            go.transform.eulerAngles = pivotGun.transform.eulerAngles + new Vector3(0, 0, 180);
        else
            go.transform.eulerAngles = pivotGun.transform.eulerAngles;
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
        animator.SetBool("IsDashing", false);
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
            animator.SetBool("IsJumping", true);
            rb.AddForce(new Vector2(0f, m_JumpForce));
            JumpCount++;
            jumping = false;
        }
    }

    internal IEnumerator RageAttack()
    {
        isRaging = true;
        animator.SetBool("RageAttackEnabled", true);

        for (int nbBullet = 0; nbBullet < 20; nbBullet++)
        {
            CreateBulletInstance(StandardBullet, Bullet.StandardDamage,true);
            yield return new WaitForSeconds(0.05f);
        }
        isRaging = false;
        animator.SetBool("RageAttackEnabled", false);

    }
    

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



