using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;

    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform; 

        rb.velocity = speed*Vector3.Normalize(target.transform.position-transform.position); 
        
    }


    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if(hitInfo.tag == "Player" || hitInfo.tag == "Wall") // test wall collider 
            Destroy(gameObject);
            // implement health manager interactions
    }
    
}
