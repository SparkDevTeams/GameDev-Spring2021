using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedProjectile : EnemyProjectile
{
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float stunTime = 0.25f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void SetDirection(Vector2 direction) 
    {
        rb.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            collision.gameObject.GetComponent<HealthManager>().damage(damage, stunTime);
        }

        Destroy(gameObject);
    }
}
