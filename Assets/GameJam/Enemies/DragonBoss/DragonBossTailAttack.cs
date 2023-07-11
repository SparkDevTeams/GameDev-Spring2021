using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossTailAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject homingProjPrefab;
    private bool attacking = false;
    public float firingDelay;
    public float shootTime;
    private float shootTimer = 0;
    public float shootSpeedMultiplier = 1;
    private int projNum = 0;
    private DragonBossAnimator animator;

    void Start()
    {
        animator = GetComponent<DragonBossAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            shootTimer += Time.deltaTime;

            if (projNum > 0)
            {                

                if (shootTimer >= (shootTime + firingDelay) / shootSpeedMultiplier)
                {
                    projNum--;
                    Shoot();
                    shootTimer = firingDelay / shootSpeedMultiplier;
                    
                }
            }
            else
            {
                if (shootTimer >= shootTime / shootSpeedMultiplier)
                {
                    StopAttack();           
                }
            }
        }        
    }

    public void StartAttack(int bulletNum, float speed = 1)
    {
        attacking = true;
        projNum = bulletNum;
        shootSpeedMultiplier = speed;
        shootTimer = shootTimer / shootSpeedMultiplier;;
        
        animator.AnimationChange(DragonState.TAIL, animator.dragonDirection, shootSpeedMultiplier, 0, 0);
    }

    public void StopAttack()
    {
        GetComponent<DragonBossManager>().attacking = false;
        attacking = false;
    }

    void Shoot()
    {
        Instantiate(homingProjPrefab, firePoint.position, firePoint.rotation);
    }
}
