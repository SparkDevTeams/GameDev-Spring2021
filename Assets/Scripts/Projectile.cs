using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : EnemyProjectile
{
    public float speed;
    public int damage;
    public Rigidbody2D rb;

    //public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        rb.velocity = speed * transform.up;
        
    }

    public override void SetDirection(Vector2 direction)
    {
        //DO NOTHING
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if(hitInfo.tag == "Player")
        {
            hitInfo.GetComponent<HealthManager>().damage(damage,0.25f);
            Destroy(gameObject);
        }
        else if(hitInfo.name == "Walls" || hitInfo.name == "Layout Walls")
        {
            Destroy(gameObject);
        }
            // test wall collider 
            
            
            // implement health manager interactions
    }
    
}
