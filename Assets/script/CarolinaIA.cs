using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarolinaIA : MonoBehaviour
{



    public Movement2D Player;

    public GameObject BalleQuiTombe;
    // Start is called before the first frame update
    Animator animator;
    public GameObject SbireTemplate;


    private Enemy Enemy;
    public float activationDistance = 15f;
    void Start()
    {
        animator = GetComponent<Animator>();
        Enemy = GetComponent<Enemy>();
        
    }


    public Transform SbiresSpawnPoint;

    DateTime TotalFightTime = DateTime.Now;
    DateTime LastShoot = DateTime.Now;

    bool WarningEnabled = false;
    bool AttackeEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if (Player == null) return;

        if (!WarningEnabled && Vector2.Distance(transform.position, Player.transform.position) <= activationDistance)
        {
            WarningEnabled = true;
            animator.enabled = true;
            TotalFightTime = DateTime.Now;
        }
        else if (!WarningEnabled) return;


        if (AttackeEnabled)
        {
            if ((DateTime.Now - LastShoot).TotalSeconds < 5)
                return;
            LastShoot = DateTime.Now;
            SbiresInstanciation();
            animator.SetBool("IsAiming", true);
            StartCoroutine(Shoot());
        }
    }

    /// <summary>
    /// Est appelé par un AnimationEvent a la fin de l'animation, carolina_warning2
    /// </summary>
    public void StartAttack()
    {
        AttackeEnabled = true;
    }

    bool WaveOneDone = false;
    bool WaveTwoDone = false;

    void SbiresInstanciation()
    {
        if (!WaveOneDone && (DateTime.Now - TotalFightTime).TotalSeconds > 2)
        {
            StartCoroutine(InstanciateSbire(3));
            WaveOneDone = true;
        }

        if (!WaveTwoDone && Enemy.Life < 500)
        {
            StartCoroutine(InstanciateSbire(5));
            WaveTwoDone = true;
        }
        


    }
    IEnumerator InstanciateSbire(int NbSbires)
    {
        for (int i = 0; i < NbSbires; i++)
        {
            GameObject newSbire = Instantiate(SbireTemplate);
            EnemyIA newIA = newSbire.GetComponent<EnemyIA>();
            newSbire.transform.position = SbiresSpawnPoint.position;
            newSbire.transform.localScale.Set(1.3f, 1.3f, 1.3f);
            newIA.SetPlayer(Player);
            yield return new WaitForSeconds(0.5f);
        }
    }

    internal IEnumerator Shoot()
    {
        yield return new WaitForSeconds(3f);

        int nbBullets = 4;
        if (Enemy.Life < 500) nbBullets = 6;
        if (Enemy.Life < 200) nbBullets = 15;


        for (int i = -nbBullets / 2; i < nbBullets / 2; i++)
        {
            GameObject newBullet = Instantiate(BalleQuiTombe);
            newBullet.transform.position = new Vector3(Player.transform.position.x + i, 40);
            newBullet.GetComponent<M79Bullet>().Player = Player.GetComponent<Player>();
            Rigidbody2D rigidBody = newBullet.GetComponent<Rigidbody2D>();
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10);
            animator.SetBool("IsAiming", false);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
