using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretScript : MonoBehaviour
{
    public float Range;

    public Transform Target;
    Player Player;
    bool Detected = false;

    Vector2 Direction;
    public GameObject Gun;

    public GameObject bullet_turret;
    public float FireRate;
    float nextTimeToFire = 4;
    public Transform ShootPoint;
    public float Force;
    public Animator turretanimator;
    // Start is called before the first frame update
    void Start()
    {
        turretanimator = GetComponent<Animator>();
        Player = Target.GetComponent<Player>(); 
    }

    // Update is called once per frame
    void Update()
    {

        //if (Target == null || Player.Dead) return;

        Vector2 targetPos = Target.position;
        Direction = targetPos - (Vector2)transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);

        if (rayInfo)
        {
            if (rayInfo.collider.gameObject.tag == "Player")

                if (Detected == false)
                {

                    Detected = true;
                    turretanimator.SetBool("EnemyDetected", true);
                }

        }
        else
        {
            if (Detected == true)
            {
                Detected = false;

            }
        }
        if (Detected)
        {
            Gun.transform.right = -Direction;
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                shoot();
            }
        }
        
        
         
        
    }
    void shoot()

    {
        GameObject BulletIns = Instantiate(bullet_turret, ShootPoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);

        BulletIns.transform.right = -Direction;
        BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);

    }
    void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, Range);
    }
}

