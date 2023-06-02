using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScriptV2 : MonoBehaviour
{
    public GameObject bullet_turret;
    public float FireRate;
    public float nextTimeToFire = 4;
    public Transform ShootPoint;
    public float Force;
    
    public Transform Target;
    private Vector2 Direction;
    public float Range;

    public bool Detected = false;
    public Animator turretanimator;

    public GameObject warning;
    private bool playerInRange = false;

    private void Start()
    {
        turretanimator = GetComponent<Animator>();
        //        turretObject = GetComponentInParent<SpriteRenderer>();// transform.GetChild(0).gameObject; // Assumant que le GameObject que tu veux faire apparaître est un enfant de la tourelle
        warning.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Target == null) return;
        Vector2 targetPos = Target.position;
        Direction = targetPos - (Vector2)transform.position;

        RaycastHit2D rayInfo;
        if (rayInfo = Physics2D.Raycast(transform.position, Direction, Range))
        {
            if (rayInfo.collider.gameObject.CompareTag("Player"))
            {
                if (!playerInRange)
                {
                    playerInRange = true;
                    StartCoroutine(ActivateAndDeactivateTurretObject(0f)); // Lancer la coroutine pour activer et désactiver le GameObject
                }

                bool canShoot = true;
                if (transform.lossyScale.x < 0f)
                {
                    // Tourrelles orientées a droite

                    transform.right = Direction;
                    float angle = (float)Math.Max(transform.eulerAngles.z, 270);
                    transform.eulerAngles = new Vector3(0f, 0f, angle);
                    canShoot = transform.eulerAngles.z > 270;
                }
                else
                {
                    // Tourrelles orientées a gauche
                    transform.right = -Direction;
                    float angle = (float)Math.Min(transform.eulerAngles.z, 90);
                    transform.eulerAngles = new Vector3(0f, 0f, angle);
                    canShoot = transform.eulerAngles.z < 90;
                }

                if (Time.time > nextTimeToFire && canShoot)
                {
                    nextTimeToFire = Time.time + 1 / FireRate;
                    Shoot();
                }
               


            }
        }
        else
        {
            if (playerInRange)
            {
                playerInRange = false;
                warning.gameObject.SetActive(false); // Désactiver le GameObject si le joueur sort de la portée de la tourelle
            }
        }
        
    }

    private void Shoot()
    {
        GameObject bulletIns = Instantiate(bullet_turret, ShootPoint.position, Quaternion.identity);
        bulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);

        bulletIns.transform.right = -Direction;
        bulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }

    private IEnumerator ActivateAndDeactivateTurretObject(float delay)
    {
        warning.gameObject.SetActive(true); // Activer le GameObject
        yield return new WaitForSeconds(2);
        warning.gameObject.SetActive(false); // Désactiver le GameObject après le délai spécifié
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}