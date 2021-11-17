using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedHomingProjectile : EnemyProjectile
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float initialTravelTime = 1.0f;
    [SerializeField]
    private float totalWaitTime = 2.5f;
    private float travelTimer = 0.0f;
    private float waitTimer = 0.0f;
    private Transform target;
    private Rigidbody2D rb;
    private bool active = false;
    private bool waiting = false;
    private bool launching = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        active = false;
        launching = false;
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            travelTimer -= Time.deltaTime;

            if (travelTimer <= 0.0f)
            {
                active = false;
                waiting = true;
                travelTimer = 0.0f;
                waitTimer = totalWaitTime;
            }
        }
        else if (waiting) 
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0.0f) 
            {
                waiting = false;
                launching = true;
                waitTimer = 0.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (waiting)
        {
            rb.velocity = Vector2.zero;

        }
        else if (launching) 
        {
            Vector2 dirToPlayer = ((Vector2)(target.position - transform.position)).normalized;
            rb.velocity = dirToPlayer * speed;
            launching = false;
        }
    }

    public override void SetDirection(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
        active = true;
        travelTimer = initialTravelTime;
    }
}
