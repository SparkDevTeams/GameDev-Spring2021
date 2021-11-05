using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator myAnim;

    public EnemyManager test;

    public Transform homePos; //in case you want enemy to return to initial position

    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    private float maxRange = 0f; 
    [SerializeField]
    private float minRange = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        test = GetComponent<EnemyManager>();
    }

    // If player is within range then the enemy will follow the player
    void FixedUpdate()
    {
        if (Vector2.Distance(test.target.position, transform.position) <= maxRange && Vector2.Distance(test.target.position, transform.position) >= minRange && !test.stunned)
        {
            FollowPlayer();
        }
    
    }

    public void FollowPlayer()
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("moveX", (test.target.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (test.target.position.y - transform.position.y));
        rb.velocity = ((Vector2)(test.target.position - transform.position)).normalized * speed;
    }
}
