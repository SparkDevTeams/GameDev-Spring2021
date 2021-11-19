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
    private bool active = false;
    private float flyTimer = 0;
    private BatBossAnimator animator;
    // Start is called before the first frame update
    void Start()
    {
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
                        
                    }
                    else {
                        //Go Life
                        animator.AnimationChange(BatState.WALK, BatDirection.LEFT);
                    }
                }
                else {
                    //Walk Up/Down
                    //Walk Side
                    if (playerTarget.position.y > transform.position.x)
                    {
                        //Go up
                        animator.AnimationChange(BatState.WALK, BatDirection.BACK);
                    }
                    else
                    {
                        animator.AnimationChange(BatState.WALK, BatDirection.FRONT);
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
