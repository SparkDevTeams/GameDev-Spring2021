using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossManager : MonoBehaviour
{
    private Transform playerTarget;
    private BatBossBreathAttack breathAttack;
    private BatBossFlyAttack flyAttack;
    private BatBossCrystalRoar roar;
    private EnemyManager manager;
    [SerializeField]
    private BatBossDivePoint centerPoint;
    private bool invincible = false;
    [SerializeField]
    private bool active = false;
    private float flyTimer = 0;
    private Rigidbody2D rigidbody;
    private BatBossAnimator animator;
    [SerializeField]
    private float walkSpeed = 6.5f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        breathAttack = GetComponent<BatBossBreathAttack>();
        flyAttack = GetComponent<BatBossFlyAttack>();
        roar = GetComponent<BatBossCrystalRoar>();
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
        //Walk
        if (Vector2.Distance(playerTarget.position, this.transform.position) > 8) {
            /*if (Vector2.Distance(playerTarget.position, this.transform.position) > 12)
            {
                //Lunge
            }
            else*/ {
                //Start Walking
                if (Mathf.Abs(playerTarget.position.x - this.transform.position.x) > Mathf.Abs(playerTarget.position.y - this.transform.position.y))
                {
                    //Walk Side
                    if (playerTarget.position.x > transform.position.x)
                    {
                        //Go right
                        animator.AnimationChange(BatState.WALK, BatDirection.RIGHT);
                        rigidbody.velocity = new Vector2(walkSpeed,0); 
                        
                    }
                    else {
                        //Go Life
                        animator.AnimationChange(BatState.WALK, BatDirection.LEFT);
                        rigidbody.velocity = new Vector2(-walkSpeed, 0);
                    }
                }
                else {
                    //Walk Up/Down
                    //Walk Side
                    if (playerTarget.position.y > transform.position.y)
                    {
                        //Go up
                        animator.AnimationChange(BatState.WALK, BatDirection.BACK);
                        rigidbody.velocity = new Vector2(0, walkSpeed);
                    }
                    else
                    {
                        animator.AnimationChange(BatState.WALK, BatDirection.FRONT);
                        rigidbody.velocity = new Vector2(0, -walkSpeed);
                        //Go down
                    }

                }
            }
        }
    }

    public void Activate() 
    {
        active = true;
        flyAttack.ActivatePatterns();
    }

    public void chooseAttack() {
        int choose = (int)Random.Range(0, 3);
        if (flyTimer > 4) {
            choose = 1;
            flyTimer = 0;
        }

        if (choose != 1) {
            flyTimer++;
        }

        switch (choose) {
            case 0:
                //BreathAttack
                
                break;
            case 1:
                //Fly
                flyTimer = 0;
                break;
            case 2:
                //Roar
                roar.Roar();
                break;
            case 3:
                //Lunge
                break;
            
        }

    } 
}
