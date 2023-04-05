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
    [SerializeField] private float m_MovementSmoothing = .05f;
    float speed = 30f;
    private float m_JumpForce = 800f;
    private bool jumping = false;
    public GameObject pivotGun;
    public GameObject spawnPoint;
    public Object bulletRef;
    public SpriteRenderer bras;
    public int JumpCount;
    private bool canDash = true;
    private float dashingPower = 15f;
    private bool isDashing;
    private float dashingTime = 0.4f;
    private float dashingCooldown = 2f;
    

    [SerializeField] private TrailRenderer tr;


    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
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
        if (Input.GetButtonDown("Jump") && jumping == false && JumpCount < 2)
        {
            jumping = true;

        }
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 v_diff = (target - transform.position).normalized;
        float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        pivotGun.transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg);

        if (pivotGun.transform.rotation.eulerAngles.z > 90 && pivotGun.transform.rotation.eulerAngles.z < 270)
        {
            bras.flipY = true;

            spawnPoint.GetComponent<Transform>().localPosition = new Vector2(1.9f, -0.35f);
        }
        else
        {
            bras.flipY = false;

            spawnPoint.GetComponent<Transform>().localPosition = new Vector2(1.9f, 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        //Debug.Log(v_diff.x);

        sr.flipX = (v_diff.x < 0);
        if (Input.GetButtonDown("Fire1"))
        {

            Instantiate(bulletRef, spawnPoint.transform.position, pivotGun.transform.rotation);
        }


    }
    private IEnumerator Dash()
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
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }

    private void FixedUpdate()
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


}



