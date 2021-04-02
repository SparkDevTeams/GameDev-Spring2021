using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;

    public Vector2 v;

    private PlayerManager t;

    public int angle;

    public float cosineTest, sineTest;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        

        t = FindObjectOfType<PlayerManager>();

        angle = t.angle;
        

        v = new Vector2(Mathf.Cos(angle * Mathf.PI/180),Mathf.Sin(angle* Mathf.PI/180));

        rb.velocity = speed * v; 
        
    }


    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if(hitInfo.tag == "Enemy" || hitInfo.tag == "Wall") // test wall collider 
            Destroy(gameObject);
            // implement health manager interactions
    }
    
}
