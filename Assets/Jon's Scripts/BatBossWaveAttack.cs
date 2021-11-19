using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossWaveAttack : MonoBehaviour
{
    private const float FULL_CIRCLE = 360.0f;

    [SerializeField]
    private List<float> shotAngles;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float angleDifference = 20.0f;
    [SerializeField]
    private float rotationSpeed = 80.0f;
    [SerializeField]
    private float timeBetweenShots = 0.125f;
    private float shotTimer = 0.0f;
    private float currDeltaAngle = 0.0f;
    private bool attacking = false;
    private bool increasing = true;

    void Start()
    {
        attacking = false;
        currDeltaAngle = 0.0f;
        shotTimer = 0.0f; 
    }

    void Update()
    {
        if (attacking) 
        {
            shotTimer -= Time.deltaTime;

            if (shotTimer <= 0.0f) 
            {
                foreach (float shotAngle in shotAngles) 
                {
                    float currShotAngle = shotAngle + currDeltaAngle;

                    if (currShotAngle > FULL_CIRCLE) 
                    {
                        currShotAngle -= FULL_CIRCLE;
                    }

                    Shoot(currShotAngle);
                }

                shotTimer = timeBetweenShots;
            }

            if (increasing)
            {
                currDeltaAngle += rotationSpeed * Time.deltaTime;

                if (currDeltaAngle > angleDifference)
                {
                    increasing = false;
                    StopAttacking();
                }
            }
            else 
            {
                currDeltaAngle -= rotationSpeed * Time.deltaTime;

                if (currDeltaAngle < 0.0f) 
                {
                    increasing = true;
                    StopAttacking();
                }
            }
        }
        
    }

    public bool IsAttacking()
    {
        return attacking;

    }

    public void Attack() 
    {
        attacking = true;

        if (increasing)
        {
            currDeltaAngle = 0.0f;
        }
        else 
        {
            currDeltaAngle = 30.0f;
        }
    }

    public void StopAttacking() 
    {
        attacking = false;
        shotTimer = 0.0f;
    }

    private void Shoot(float shotAngle) 
    {
        Vector2 shotDir = new Vector2(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
        EnemyProjectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
        newProjectile.SetDirection(shotDir);
    }

}
