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

    private GameObject turretObject;
    private bool playerInRange = false;

    private void Start()
    {
        turretanimator = GetComponent<Animator>();
        turretObject = transform.GetChild(0).gameObject; // Assumant que le GameObject que tu veux faire apparaître est un enfant de la tourelle
        turretObject.SetActive(false);
    }

    private void Update()
    {
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

                transform.right = -Direction;
                if (Time.time > nextTimeToFire)
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
                turretObject.SetActive(false); // Désactiver le GameObject si le joueur sort de la portée de la tourelle
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
        turretObject.SetActive(true); // Activer le GameObject
        yield return new WaitForSeconds(2);
        turretObject.SetActive(false); // Désactiver le GameObject après le délai spécifié
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}