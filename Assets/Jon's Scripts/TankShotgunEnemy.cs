using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShotgunEnemy : MonoBehaviour
{
    private EnemyManager manager = null;
    private SpriteRenderer sprite = null;
    private Vector2 currentShotDir;
    [SerializeField]
    private int totalShots = 12;
    private int shotsLeft = 0;
    [SerializeField]
    private float timeBetweenShots = 0.15f;
    [SerializeField]
    private float totalArmorTime = 4.0f;
    [SerializeField]
    private float totalVulnerableTime = 2.8f;
    [SerializeField]
    private float maxAngleDifference = 15.0f;
    [SerializeField]
    private GameObject fixedProjectile;
    [SerializeField]
    Color testArmoredColor;
    [SerializeField]
    Color testActiveColor;
    private float shotTimer = 0.0f;
    private float armorTimer = 0.0f;
    private float vulnerableTimer = 0.0f;
    private bool shooting = false;
    private bool armored = true;

    void Start()
    {
        manager = GetComponent<EnemyManager>();
        sprite = GetComponent<SpriteRenderer>();
        manager.TriggerInvincibility();
        shotsLeft = 0;
        shooting = false;
        armored = true;
        shotTimer = 0.0f;
        armorTimer = totalArmorTime;
        vulnerableTimer = 0.0f;
        sprite.color = testArmoredColor;
    }

    void Update()
    {
        if (!shooting && armored)
        {
            armorTimer -= Time.deltaTime;

            if (armorTimer <= 0.0f)
            {
                ActiveMode();
                armorTimer = 0.0f;
            }
        }
       
        if (shooting) 
        {
            if (shotsLeft > 0) 
            {
                shotTimer -= Time.deltaTime;

                if (shotTimer <= 0.0f) 
                {
                    Shoot();
                    shotsLeft--;

                    if (shotsLeft > 0)
                    {
                        shotTimer = timeBetweenShots;
                    }
                    else 
                    {
                        shotTimer = 0.0f;
                        shooting = false;
                    }
                }
            }
        }

        if (!armored) 
        {
            vulnerableTimer -= Time.deltaTime;

            if (vulnerableTimer <= 0.0f) 
            {
                DefensiveMode();
                vulnerableTimer = 0.0f;
            }
        }
        
    }

    private void ActiveMode() 
    {
        sprite.color = testActiveColor;
        armored = false;
        shooting = true;
        manager.StopInvincibility();
        vulnerableTimer = totalVulnerableTime;
        shotsLeft = totalShots;
        currentShotDir = ((Vector2)(manager.target.position - transform.position)).normalized;
    }

    private void DefensiveMode() 
    {
        sprite.color = testArmoredColor;
        armored = true;
        manager.TriggerInvincibility();
        armorTimer = totalArmorTime;
    }

    private void Shoot() 
    {
        float currentAngle = Mathf.Atan2(currentShotDir.y, currentShotDir.x) * Mathf.Rad2Deg;
        currentAngle = Random.Range(currentAngle - maxAngleDifference, currentAngle + maxAngleDifference);
        EnemyProjectile currProjectile = Instantiate(fixedProjectile, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
        currProjectile.SetDirection(new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)));
    }
}
