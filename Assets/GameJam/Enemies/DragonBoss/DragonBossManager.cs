using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossManager : MonoBehaviour
{
    private Transform playerTarget;
    private DragonBossLaserAttack laserAttack;
    private DragonBossFireballAttack fireballAttack;
    private DragonBossFlyAttack flyAttack;
    private DragonBossTailAttack tailAttack;
    private EnemyManager manager;
    [SerializeField]
    private bool active = false;
    
    public bool attacking = false;
    
    private float attackTimer = 0;
    [SerializeField] private float attackTime = 2.0f;
    [SerializeField] private float waitTime = 1.0f;
    private float waitTimer = 0.0f;
    private Rigidbody2D rb;
    private BatBossAnimator animator;
    [SerializeField]
    private float walkSpeed = 6.5f;
    private int phaseNum = 1;
    private bool newPhase = true;
    private bool fireballed = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        laserAttack = GetComponent<DragonBossLaserAttack>();
        fireballAttack = GetComponent<DragonBossFireballAttack>();
        flyAttack = GetComponent<DragonBossFlyAttack>();
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
        if ((float)manager.hp / (float)manager.startHp <= 0.5f && phaseNum < 2) //10% hp left
        {
            phaseNum = 4;
            newPhase = true;
        }
        else if ((float)manager.hp / (float)manager.startHp <= 0.25f && phaseNum < 2) //25% hp left
        {
            phaseNum = 3;
            newPhase = true;
        }
        else if ((float)manager.hp / (float)manager.startHp <= 0.5f && phaseNum < 2) //50% hp left
        {
            phaseNum = 2;
            newPhase = true;
        }

        //Walk
        if (Vector2.Distance(playerTarget.position, this.transform.position) > 25 && waitTimer <= 0)
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
        int choose = Random.Range(0, 3);

        if (newPhase)
        {
            choose = 4;
            newPhase = false;
        }
        else if (!fireballed && phaseNum >= 2)
        {
            choose = 3;
        }   

        attacking = true;

        //Priority : Fly->Fireball->Tail/Laser
        //Laser twice as common as Tail
        //Fly happens at start of each phase
        //Fireball happens at start of phase 2

        switch (choose) {
            case 0:
            case 1:
                //Laser attack
                attacking = false;
                break;
            case 2:
                //Tail attack
                tailAttack.StartAttack(4 * phaseNum);
                break;
            case 3:
                //Fireball attack
                fireballed = true;

                fireballAttack.StartAttack();
                break;
            case 4:
                //Fly attack
                flyAttack.StartFlying(Random.Range(0, flyAttack.FlightPatternListSize()));
                break;
        }

    }
}
