using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator myAnim;
    private Transform target;

    public Transform homePos; //in case you want enemy to return to initial position

    private const float BUFFER = 0.001f;

    [SerializeField]
    private bool eyesightEnabled = false;
    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    private float maxRange = 0f;
    [SerializeField]
    private float minRange = 0f;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        active = false;
    }

    // If player is within range then the enemy will follow the player
    void FixedUpdate()
    {
        if (!active && eyesightEnabled)
        {
            active = IsPlayerInSightRange();
        }

        if (active || !eyesightEnabled) 
        {
            float distToPlayer = DistanceTo2D(target.position);

            if (distToPlayer > minRange + BUFFER)
            {
                FollowPlayer();
            }
            else if (distToPlayer < minRange - BUFFER)
            {
                RunFromPlayer();
            }
            else 
            {
                StopMoveAnimation(DirectionTowards2D(target.position));
            }
        }
    }

    private Vector2 DirectionTowards2D(Vector2 targetPos)
    {
        return (targetPos - (Vector2)transform.position).normalized;
    }

    private float DistanceTo2D(Vector2 targetPos)
    {
        return (targetPos - (Vector2)transform.position).magnitude;
    }

    private bool IsPlayerInSightRange()
    {
        return DistanceTo2D(target.position) <= maxRange;
    }

    //Enemy follow movement
    private void FollowPlayer()
    {
        Vector2 dirToPlayer = DirectionTowards2D(target.position);
        StartMoveAnimation(dirToPlayer);
        rb.velocity = (Vector3)dirToPlayer * speed;
    }

    //Enemy run away movement
    private void RunFromPlayer() 
    {
        Vector2 dirToPlayer = DirectionTowards2D(target.position);
        StartMoveAnimation(dirToPlayer);
        rb.velocity = (Vector3)dirToPlayer * -speed;
    }

    private void StopMoveAnimation(Vector2 direction) 
    {
        myAnim.SetFloat("moveX", (direction.x));
        myAnim.SetFloat("moveY", (direction.y));
        myAnim.SetBool("isMoving", false);
    }

    private void StartMoveAnimation(Vector2 direction)
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("moveX", (direction.x));
        myAnim.SetFloat("moveY", (direction.y));
    }

    //optional function
    /*public void GoHome()
    {
        const float EPSILON = 0.001f;

        myAnim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (homePos.position.y - transform.position.y));
        Vector2 dirToHome = (homePos.position - transform.position).normalized;
        transform.position += (Vector3)dirToHome * speed * Time.deltaTime;

        //enemy goes back to home position and stops moving
        if (DistanceTo2D(homePos.position) <= EPSILON)
        {
            myAnim.SetBool("isMoving", false);
        }
    }*/
}
