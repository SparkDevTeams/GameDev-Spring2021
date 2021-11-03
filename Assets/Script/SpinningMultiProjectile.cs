using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningMultiProjectile : EnemyProjectile
{
    private const float FULL_CIRCLE = 360.0f;
    [SerializeField]
    private List<GameObject> subProjectiles;
    [SerializeField]
    private List<float> degreesToShoot; 
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float speed = 7.2f;
    [SerializeField]
    private float spinSpeedInDegrees = 6.0f;
    [SerializeField]
    private float timeBetweenShots = 0.5f;
    [SerializeField]
    private float stunTime = 0.25f;
    [SerializeField]
    private bool destroyOnPlayerContact = false;
    private bool shooting = false;
    private float shotTimer = 0.0f;
    private float degreeOffset = 0.0f;
    private int currentProjectileIndex = 0;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shotTimer = 0.0f;
        degreeOffset = 0.0f;
        shooting = false;
    }

    void Update()
    {
        if (!shooting) 
        {
            shotTimer -= Time.deltaTime;

            if (shotTimer <= 0.0f) 
            {
                shooting = true;
                shotTimer = timeBetweenShots;
            }
        }

        if (shooting) 
        {
            foreach (float angle in degreesToShoot) 
            {
                float angleToShoot = angle + degreeOffset;

                if (angleToShoot >= FULL_CIRCLE) 
                {
                    angleToShoot -= FULL_CIRCLE;
                }

                ShootSubProjectile(subProjectiles[currentProjectileIndex], angleToShoot);

                currentProjectileIndex = (currentProjectileIndex + 1) % subProjectiles.Count;
            }

            shooting = false;
        }

        degreeOffset += spinSpeedInDegrees * Time.deltaTime;

        if (degreeOffset >= FULL_CIRCLE) 
        {
            degreeOffset -= FULL_CIRCLE;
        }
    }

    public override void SetDirection(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<HealthManager>().damage(damage, stunTime);

            if (destroyOnPlayerContact) 
            {
                Destroy(gameObject);
            }
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void ShootSubProjectile(GameObject subProjectile, float angleDegrees) 
    {
        Vector2 shotDir = new Vector2(Mathf.Cos(angleDegrees * Mathf.Deg2Rad), Mathf.Sin(angleDegrees * Mathf.Deg2Rad));
        EnemyProjectile newSubProjectile = Instantiate(subProjectile, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
        newSubProjectile.SetDirection(shotDir);
    }
}
