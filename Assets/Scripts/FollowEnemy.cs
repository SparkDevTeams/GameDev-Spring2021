using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{

    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;


    private Animator anim;                                  //Modified      Add an "Attacking" parameter to the enemy animator
    public float attackDistance;                            //Modified
    public float stoppingDistance;                          //Modified      To stop the enemy from continously moving into the player when in attcking range
    private bool attacking;                                 //Modified

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();                    //Modified
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        //direction.Normalize();
        movement = direction;
#region                                                     //Expand region for modified code
        if(Vector2.Distance(transform.position, player.position) < attackDistance){     //Checks if enemy is within attacking range
            attacking = true;
        } else attacking = false;
        anim.SetBool("Attacking", attacking);               //Sets "Attacking" parameter based on distance between player and enemy. Remember to loop animation
#endregion

    }

    private void FixedUpdate()
    {
        moveCharacter(movement);

    }

    void moveCharacter(Vector2 dir)     //Modified direction to dir to avoid confusion with previoudsly declared direction
    {
        //rb.MovePosition((Vector2)transform.position + (dir * moveSpeed * Time.deltaTime));

        //Modified code below
        if((Vector2.Distance(transform.position, player.position)) > stoppingDistance && !GetComponent<EnemyManager>().stunned){

            //Checks to stop moving when close to the player
            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else {
                transform.localScale = new Vector3(1, 1, 1);
            }
            rb.velocity = dir.normalized * moveSpeed;   //Modified movement for constant speed but you can use your own. I just added it as an alternative
        } else rb.velocity = Vector2.zero;       
    }

    private void OnDrawGizmos() {                   //Blue gizmos for enemy stop radius
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }

}
