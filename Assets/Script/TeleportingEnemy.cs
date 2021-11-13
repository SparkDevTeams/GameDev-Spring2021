using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingEnemy : MonoBehaviour
{
    private EnemyManager manager;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private List<CircleCollider2D> circleRanges;
    [SerializeField]
    private List<Transform> teleportPoints;
    private List<Transform> adjustedList;
    [SerializeField]
    private List<float> projectileAngleVariations;
    [SerializeField]
    private bool useCircleRange = false;
    [SerializeField]
    private float totalActiveTime = 3.0f;
    [SerializeField]
    private float totalEscapeTime = 0.6f;
    [SerializeField]
    private float totalStartUpTime = 1.0f;
    [SerializeField]
    private float totalReloadTime = 0.8f;
    private float activeTimer = 0.0f;
    private float escapeTimer = 0.0f;
    private float startUpTimer = 0.0f;
    private float reloadTimer = 0.0f;


    void Start()
    {
        manager = GetComponent<EnemyManager>();
        adjustedList.AddRange(teleportPoints);
        activeTimer = 0.0f;
        escapeTimer = 0.0f;
        startUpTimer = totalStartUpTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (startUpTimer > 0.0f) 
        {
            startUpTimer -= Time.deltaTime;

            if (startUpTimer <= 0.0f) 
            {
                Vector2 shotDir = GetShotDirToPlayer();

                foreach (float angleDifference in projectileAngleVariations) 
                {
                    ShootAtPlayer(shotDir, angleDifference);
                }
                activeTimer = totalActiveTime;
                reloadTimer = totalReloadTime;
            }
        }

        if (activeTimer > 0.0f) 
        {
            activeTimer -= Time.deltaTime;

            if (activeTimer <= 0.0f)
            {
                manager.TriggerInvincibility();
                escapeTimer = totalEscapeTime;
            }
            else 
            {
                reloadTimer -= Time.deltaTime;

                if (reloadTimer <= 0.0f) 
                {
                    Vector2 shotDir = GetShotDirToPlayer();

                    foreach (float angleDifference in projectileAngleVariations)
                    {
                        ShootAtPlayer(shotDir, angleDifference);
                    }
                    reloadTimer = totalReloadTime;
                }
            }
        }

        if(escapeTimer > 0.0f) 
        {
            escapeTimer -= Time.deltaTime;

            if(escapeTimer <= 0.0f) 
            {
                if (useCircleRange) 
                {
                    RelocateCircle();
                }
                else 
                {
                    RelocatePoint();
                }

                manager.StopInvincibility();
                startUpTimer = totalStartUpTime;
            }
        }
    }

    private void ShootAtPlayer(Vector2 baseShotDir, float angleDifference) 
    {
        float shotAngle = (Mathf.Atan2(baseShotDir.y, baseShotDir.x) * Mathf.Rad2Deg) + angleDifference;
        EnemyProjectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
        newProjectile.SetDirection(new Vector2(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad)));
    }

    private void RelocatePoint() 
    {
        int pointIndex = Random.Range(0, adjustedList.Count);
        Transform selectedPoint = adjustedList[pointIndex];
        adjustedList.Clear();
        adjustedList.AddRange(teleportPoints);
        adjustedList.Remove(selectedPoint);
        transform.position = selectedPoint.position;
    }

    private void RelocateCircle() 
    {
        int circleIndex = Random.Range(0, circleRanges.Count);
        transform.position = (Vector2)circleRanges[circleIndex].transform.position
            + Random.insideUnitCircle * circleRanges[circleIndex].radius;
    }

    private Vector2 GetShotDirToPlayer() 
    {
        return (Vector2)(manager.target.position - transform.position).normalized;
    }
}
