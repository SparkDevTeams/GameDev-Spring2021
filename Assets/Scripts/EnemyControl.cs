using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator myAnim;

    public EnemyManager manager;

    private const float BUFFER = 0.001f;

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

        manager = GetComponent<EnemyManager>();
    }

    // If player is within range then the enemy will follow the player
    void FixedUpdate()
    {
        if (!manager.stunned)
        {
            float distToPlayer = DistanceTo2D(manager.target.position);

            if (distToPlayer > maxRange + BUFFER)
            {
                FollowPlayer();
            }
            else if (distToPlayer < minRange - BUFFER)
            {
                RunFromPlayer();
            }
            else
            {
                rb.velocity = Vector2.zero;
                StopMoveAnimation(DirectionTowards2D(manager.target.position));
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
        return DistanceTo2D(manager.target.position) <= maxRange;
    }

    //Enemy follow movement
    private void FollowPlayer()
    {
        Vector2 dirToPlayer = DirectionTowards2D(manager.target.position);
        StartMoveAnimation(dirToPlayer);
        rb.velocity = (Vector3)dirToPlayer * speed;
    }

    //Enemy run away movement
    private void RunFromPlayer()
    {
        Vector2 dirToPlayer = DirectionTowards2D(manager.target.position);
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
}
