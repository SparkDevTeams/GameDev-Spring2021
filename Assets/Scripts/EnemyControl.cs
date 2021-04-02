using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Animator myAnim;

    public Transform target;

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
        myAnim = GetComponent<Animator>();

        test = GetComponent<EnemyManager>();
        
        target = test.target;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        //homePos.position = transform.position;

    }

    // If player is within range then the enemy will follow the player
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange && !test.stunned)
        {
            FollowPlayer();
        }
    
    }

    public void FollowPlayer()
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

}
