using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject projectile;
    public bool canShoot;

    [SerializeField]private float arrowCooldown;
    private float currentTimer;

    private PlayerManager player;

   void Start()
   {
        player = GetComponent<PlayerManager>();
        canShoot = true;
        currentTimer = 0;
   }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire2") && canShoot)
        {
            Shoot();
            GetComponent<move>().SetAnimation("Magic", 0.25f, true);
        }

        if (!canShoot)
        {
            currentTimer += Time.deltaTime;

            if(currentTimer >= arrowCooldown)
            {
                canShoot = true;
                currentTimer = 0;
            }
        }

    }

    void Shoot()
    {
        Instantiate(projectile, firePoint.position, Quaternion.Euler(new Vector3(0,0, player.angle)));
        canShoot = false;
    }
}
