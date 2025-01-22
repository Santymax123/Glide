using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float xRange = 20f;
    public float yRange = 20f;
    public float gunShotRadius = 20f;

    public float bigDamage = 2f;
    public float smallDamage = 1f;

    public float fireRate = 1f;
    private float nextTimeToFire;

    public int maxAmmo;
    private int ammo;

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;

    private BoxCollider gunTrigger;
    public EnemyManager enemyManager;
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, yRange, xRange);
        gunTrigger.center = new Vector3(0, 0, xRange * 0.5f);
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire && ammo > 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        // simulate gun shot radius

        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

        // alert any enemy in earshot
        foreach (var enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        }


        //play audio
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();


        // damage enemies
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, xRange * 1.5f, raycastLayerMask))
            {
                if (hit.transform == enemy.transform)
                {
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if (dist > xRange * 0.5f)
                    {
                        enemy.TakeDamage(smallDamage);
                    }
                    else
                    {
                        enemy.TakeDamage(bigDamage);
                    }
                }
            }
        }

        //reset timer
        nextTimeToFire = Time.time + fireRate;

        // lose ammo
        ammo--;
    }


    public void GiveAmmo(int amount, GameObject pickup)
    {
        if (ammo < maxAmmo)
        {
            ammo += amount;
            Destroy(pickup);
        }

        
        if (ammo > maxAmmo)
        {
            ammo = maxAmmo;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // add potential enemy  
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // add potential enemy  
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }

    public int GetAmmo()
    {
        return ammo;
    }
}