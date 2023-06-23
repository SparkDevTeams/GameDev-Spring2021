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
    public float arrowCooldownMultiplier;
    private float currentTimer;

    private PlayerManager player;

   void Start()
   {
        player = GetComponent<PlayerManager>();
        canShoot = true;
        currentTimer = 0;
        arrowCooldownMultiplier = 1;
   }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<move>().Mode == "Dead") { return; }

        if (move.gameIsPaused == false && Input.GetButtonDown("Fire2") && canShoot)
        {
            Shoot();
            GetComponent<move>().SetAnimation("Magic", 0.25f, true);
        }

        if (!canShoot)
        {
            currentTimer += Time.deltaTime;

            if(currentTimer >= arrowCooldown * arrowCooldownMultiplier)
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
