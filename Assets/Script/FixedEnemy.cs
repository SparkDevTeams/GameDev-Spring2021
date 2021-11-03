using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedEnemy : MonoBehaviour
{
    private const float EPSILON = 0.001f;
    private EnemyManager manager;
    [SerializeField]
    private float speed = 8.0f;
    [SerializeField]
    private float totalReloadTime = 0.5f;
    [SerializeField]
    private List<Transform> points;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private List<float> degreesToShoot;
    [SerializeField]
    private bool isTargetingPlayer = false;
    private bool shooting = false;
    private float reloadTimer = 0.0f;
    private int currentPointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<EnemyManager>();
        reloadTimer = totalReloadTime;
        shooting = false;
        currentPointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Count > 0) 
        {
            MoveToPoint();
        }

        if (!shooting) 
        {
            reloadTimer -= Time.deltaTime;

            if (reloadTimer <= 0.0f) 
            {
                shooting = true;
                reloadTimer = totalReloadTime;
            }
        }

        if (shooting) 
        {
            if (!isTargetingPlayer)
            {
                ShootAtPlayer();
            }
            else
            {
                ShootAllDegreeShots();
            }

            shooting = false;
        }
    }

    private void MoveToPoint() 
    {
        transform.position = Vector2.MoveTowards(transform.position, points[currentPointIndex].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, points[currentPointIndex].position) <= EPSILON) 
        {
            currentPointIndex = (currentPointIndex + 1) % points.Count;
        }
    }

    private void ShootAtPlayer() 
    {
        Vector2 shotDir = (Vector2)(manager.target.position - transform.position).normalized;
        EnemyProjectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
        newProjectile.SetDirection(shotDir);
    }

    private void ShootAtDegrees(float angleDegrees) 
    {
        Vector2 shotDir = new Vector2(Mathf.Cos(angleDegrees * Mathf.Deg2Rad), Mathf.Sin(angleDegrees * Mathf.Deg2Rad));
        EnemyProjectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
        newProjectile.SetDirection(shotDir);
    }

    private void ShootAllDegreeShots() 
    {
        foreach(float angle in degreesToShoot)
        {
            ShootAtDegrees(angle);
        }
    }
}
