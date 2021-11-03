using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedHomingProjectile : EnemyProjectile
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float initialTravelTime = 0.8f;
    [SerializeField]
    private float waitTime = 2.5f;
    private float travelTimer = 0.0f;
    private float waitTimer = 0.0f;
    private Transform target;
    private Rigidbody2D rb;
    private bool active = false;
    private bool launching = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        active = false;
        launching = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetDirection(Vector2 direction)
    {
        
    }
}
