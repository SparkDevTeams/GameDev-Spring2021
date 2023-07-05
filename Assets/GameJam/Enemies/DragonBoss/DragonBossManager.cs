using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossManager : MonoBehaviour
{
    private Transform playerTarget;
    private DragonBossLaserAttack laserAttack;
    private DragonBossFireballAttack fireballAttack;
    private DragonBossFlyAttack flyAttack;
    private DragonBossLandingAttack landingAttack;
    private DragonBossTailAttack tailAttack;
    private EnemyManager manager;
    private bool invincible = false;
    [SerializeField]
    private bool active = false;
    
    public bool attacking = false;
    
    private float flyTimer = 0;
    private float attackTimer = 0;
    private float attackTime = 2.0f;
    private float waitTime = 1.0f;
    private float waitTimer = 0.0f;
    private Rigidbody2D rb;
    private BatBossAnimator animator;
    [SerializeField]
    private float walkSpeed = 6.5f;
    private int phaseNum = 1;
    private bool newPhase = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        laserAttack = GetComponent<DragonBossLaserAttack>();
        fireballAttack = GetComponent<DragonBossFireballAttack>();
        flyAttack = GetComponent<DragonBossFlyAttack>();
        landingAttack = GetComponent<DragonBossLandingAttack>();
        tailAttack = GetComponent<DragonBossTailAttack>();

        manager = GetComponent<EnemyManager>();
        animator = GetComponent<BatBossAnimator>();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) {
            return;
        }

        //check phase
        if ((float)manager.hp / (float)manager.startHp < 0.5f && phaseNum < 2) //50% hp left
        {
            phaseNum = 2;
            newPhase = true;
        }

        //Walk
        if (Vector2.Distance(playerTarget.position, this.transform.position) > 6 && waitTimer <= 0)
        {
            if (attacking) 
            { 
                return; 
            }

            //Start Walking
            if (Mathf.Abs(playerTarget.position.x - this.transform.position.x) > Mathf.Abs(playerTarget.position.y - this.transform.position.y))
            {
                //Walk Side
                if (playerTarget.position.x > transform.position.x)
                {
                    //Go right
                    animator.AnimationChange(BatState.WALK, BatDirection.RIGHT);
                    rb.velocity = new Vector2(walkSpeed, 0);

                }
                else
                {
                    //Go Left
                    animator.AnimationChange(BatState.WALK, BatDirection.LEFT);
                    rb.velocity = new Vector2(-walkSpeed, 0);
                }
            }
            else
            {
                //Walk Up/Down
                //Walk Side
                if (playerTarget.position.y > transform.position.y)
                {
                    //Go up
                    animator.AnimationChange(BatState.WALK, BatDirection.BACK);
                    rb.velocity = new Vector2(0, walkSpeed);
                }
                else
                {
                    animator.AnimationChange(BatState.WALK, BatDirection.FRONT);
                    rb.velocity = new Vector2(0, -walkSpeed);
                    //Go down
                }

            }
            
        }
        else {
            if (attacking) 
            { 
                return;
            }

            //Attack/Idle
            rb.velocity = Vector2.zero;
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackTime)
            {
                attackTimer = 0;
                waitTime = 0;
                chooseAttack();
            }
            else {
                animator.AnimationChange(BatState.IDLE, animator.BatDirection);
                
                Debug.Log("Boss is Waiting to attack");

            }
            if (waitTimer >= waitTime)
            {
                waitTimer = 0;
            }
            else {
                waitTimer += Time.deltaTime;
            }

        }
    }

    public void Activate() 
    {
        active = true;
        flyAttack.ActivatePatterns();
        chooseAttack();
    }

    public void chooseAttack() {
        //fly attack when enter new phase
        int choose;
        if (phaseNum > 1)
        {
            choose = Random.Range(0, 3);
        }
        else
        {
            choose = Random.Range(0, 2);
        }

        if (newPhase)
        {
            choose = 4;
            newPhase = false;
        }        

        attacking = true;

        switch (choose) {
            case 0:
                //Breath attack
                attacking = false;
                break;
            case 1:
                //Tail attack
                attacking = false;
                break;
            case 2:
                //Fireball attack
                attacking = false;
                break;
            case 4:
                //Fly attack
                flyAttack.StartFlying(Random.Range(0, flyAttack.FlightPatternListSize()));
                break;
        }

    }
}
