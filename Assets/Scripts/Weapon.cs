using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
   public Transform[] spawnLocations;
   public GameObject projectile;

   public EnemyManager test;

   public Transform target;

   public int minRange;

   public float fireRate;

   private float timer = 0;

   float bulletSpeed = 10f;

   void Start()
   {
       
       test = GetComponent<EnemyManager>();
       target = test.target;
       target = GameObject.FindGameObjectWithTag("Player").transform;
   }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) < minRange && fireRate <= timer && !test.stunned)
        // for player Input.GetButtonDown();
        // if(Input.GetButtonDown())
        {
            Shoot();
            timer = 0;
        }
        timer += Time.deltaTime;

    }

    void Shoot()
    {
        foreach (Transform location in spawnLocations)
        {
            Instantiate(projectile, location.position, location.rotation);
        }
    }
}

