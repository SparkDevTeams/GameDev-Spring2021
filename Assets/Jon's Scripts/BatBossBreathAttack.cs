using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossBreathAttack : MonoBehaviour
{
    private const float FULL_CIRCLE = 360.0f;

    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float angleDifference = 30.0f;
    [SerializeField]
    private float rotationSpeed = 80.0f;
    [SerializeField]
    private float totalAttackTime = 3.4f;
    [SerializeField]
    private float timeBetweenShots = 0.125f;
    private float attackTimer = 0.0f;
    private float shotTimer = 0.0f;
    private float currDeltaAngle = 0.0f;
    private float currBaseAngle = 0.0f;
    [SerializeField]
    private bool attacking = false;
    private bool increasing = true;

    void Start()
    {
        attacking = false;
        currBaseAngle = 0.0f;
        currDeltaAngle = 0.0f;
        attackTimer = 0.0f;
        shotTimer = 0.0f; 
    }

    void Update()
    {
        if (attacking) 
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0.0f) 
            {
                StopAttacking();
            }
            else 
            {
                shotTimer -= Time.deltaTime;

                if(shotTimer <= 0.0f) 
                {
                    float shotAngle = currBaseAngle + currDeltaAngle;

                    if (shotAngle > FULL_CIRCLE) 
                    {
                        shotAngle -= FULL_CIRCLE;
                    }

                    Shoot(shotAngle);
                    shotTimer = timeBetweenShots;
                }

                if (increasing)
                {
                    currDeltaAngle += rotationSpeed * Time.deltaTime;

                    if (currDeltaAngle > angleDifference)
                    {
                        currDeltaAngle = angleDifference;
                        increasing = false;
                    }
                }
                else 
                {
                    currDeltaAngle -= rotationSpeed * Time.deltaTime;

                    if (currDeltaAngle < -angleDifference)
                    {
                        currDeltaAngle = -angleDifference;
                        increasing = true;
                    }
                }
            }
        }
        
    }

    public bool IsAttacking()
    {
        return attacking;

    }

    public void Attack(Vector2 baseShotDir) 
    {
        attacking = true;
        increasing = true;
        attackTimer = totalAttackTime;
        currDeltaAngle = -angleDifference;
        currBaseAngle = (Mathf.Atan2(baseShotDir.y, baseShotDir.x) * Mathf.Rad2Deg);
    }

    public void Attack(Vector2 baseShotDir, Transform newFirePoint) 
    {
        firePoint = newFirePoint;
        Attack(baseShotDir);
    }

    public void StopAttacking() 
    {
        attacking = false;
        attackTimer = 0.0f;
        shotTimer = 0.0f;
        currDeltaAngle = 0.0f;
        currBaseAngle = 0.0f;
    }

    private void Shoot(float shotAngle) 
    {
        Vector2 shotDir = new Vector2(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
        EnemyProjectile newProjectile = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
        newProjectile.SetDirection(shotDir);
    }
}
