using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator myAnim;
    private Transform target;

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
        target = FindObjectOfType<PlayerController>().transform;
    }

    // If player is within range then the enemy will follow the player
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            FollowPlayer();
        }

        //optional
        else if (Vector3.Distance(target.position, transform.position) >= maxRange)
        {
            GoHome();
        }
    }

    //Enemy movement
    public void FollowPlayer()
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    //optional function
    public void GoHome()
    {
        myAnim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (homePos.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);

        //enemy goes back to home position and stops moving
        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            myAnim.SetBool("isMoving", false);
        }
    }

}
