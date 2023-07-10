using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossTailAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject homingProjPrefab;
    private bool attacking = false;
    public float shootTime;
    private float shootTimer = 0;
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
            if (projNum > 0)
            {
                if (shootTimer == 0)
                {
                    animator.AnimationChange(DragonState.TAIL, animator.dragonDirection);
                }
                
                shootTimer += Time.deltaTime;

                if (shootTimer >= shootTime)
                {
                    projNum--;
                    Shoot();
                    shootTimer = 0;
                }
            }
            else
            {
                StopAttack();
            }
        }        
    }

    public void StartAttack(int bulletNum)
    {
        attacking = true;
        projNum = bulletNum;
        shootTimer = 0;
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
