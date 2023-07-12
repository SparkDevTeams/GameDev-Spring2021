using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossLaserAttack : MonoBehaviour
{
    DragonBossManager bossManager;
    private DragonBossAnimator animator;
    private Rigidbody2D rb;
    [SerializeField]
    private Transform laserTransform;    
    [SerializeField]
    private GameObject laserSpriteObj;
    [SerializeField]
    private GameObject laserHitboxObj;
    [SerializeField]
    private float laserAppearTime;
    [SerializeField]
    private float totalChargeTime;
    [SerializeField]
    private float totalMaxSizeLaserTime;
    [SerializeField]
    private float totalMaxLaserTime;
    [SerializeField]
    private float totalAttackTime;
    [SerializeField]
    private float maxScaleY = 2;
    private float chargeTimer = 0;
    private float attackTimer = 0;
    private bool isCharging = false;
    private bool isAttacking = false;
    bool atkLeft = false;
    public Transform centerLine;
    public Transform playerTransform;
    public Vector3 leftOffset;
    bool lasered = false;
    public float atkSpeedMultiplier = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<DragonBossAnimator>();
        bossManager = GetComponent<DragonBossManager>();
    }

    // Update is called once per frame
    void Update()
    {
        laserHitboxObj.transform.localPosition = new Vector3(laserHitboxObj.transform.localPosition.x, 0, laserHitboxObj.transform.localPosition.z);

        if (isCharging)
        {
            if (DragonBossManager.AlmostEqual(bossManager.targetPos, transform.position, 1e-3f))
            {
                if (chargeTimer == 0)
                {
                    bossManager.lockedTarget = false;
                    rb.velocity = Vector2.zero;
                    
                    if (atkLeft)
                    {
                        animator.AnimationChange(DragonState.LASER, DragonDirection.RIGHT, 2 * atkSpeedMultiplier);
                    }
                    else
                    {
                        animator.AnimationChange(DragonState.LASER, DragonDirection.LEFT, 2 * atkSpeedMultiplier);
                    }
                }
                else if (chargeTimer >= laserAppearTime / atkSpeedMultiplier && !lasered)
                {                    
                    lasered = true;
                    laserSpriteObj.SetActive(true);
                    laserHitboxObj.SetActive(true);
                    laserTransform.localScale = new Vector3(laserTransform.localScale.x, 0.01f, laserTransform.localScale.z);
                }

                if (chargeTimer < totalChargeTime / atkSpeedMultiplier)
                {
                    chargeTimer += Time.deltaTime;
                    if (lasered)
                    {
                        //Scale up based on charge time
                        float scaleY = maxScaleY * (chargeTimer / (totalChargeTime / atkSpeedMultiplier));
                        if (scaleY > maxScaleY)
                        {
                            scaleY = maxScaleY;
                        }
                        laserTransform.localScale = new Vector3(laserTransform.localScale.x, scaleY, laserTransform.localScale.z);
                    }                    
                }
                else
                {
                    laserTransform.localScale = new Vector3(laserTransform.localScale.x, maxScaleY, laserTransform.localScale.z);
                    //start attack
                    isAttacking = true;
                    isCharging = false;
                }
            }        
        }
        else if (isAttacking)
        {
            if (attackTimer < totalAttackTime / atkSpeedMultiplier)
            {
                attackTimer += Time.deltaTime;

                if (attackTimer > totalMaxSizeLaserTime / atkSpeedMultiplier)
                {
                    //Scale up based on charge time
                    float scaleY = maxScaleY - (maxScaleY - 0.01f) * ((attackTimer - totalMaxSizeLaserTime / atkSpeedMultiplier) / ((totalMaxLaserTime - totalMaxSizeLaserTime) / atkSpeedMultiplier));
                    if (scaleY <= 0.01f)
                    {
                        scaleY = 0.01f;
                        laserSpriteObj.SetActive(false);
                        laserHitboxObj.SetActive(false);
                    }
                    laserTransform.localScale = new Vector3(laserTransform.localScale.x, scaleY, laserTransform.localScale.z);
                }
            }
            else
            {
                laserTransform.localScale = new Vector3(laserTransform.localScale.x, 0.01f, laserTransform.localScale.z);
                //end attack
                StopAttack();
            }
        }        
    }

    public Vector3 StartAttack(float speed = 1) //charge
    {
        attackTimer = 0;
        chargeTimer = 0;
        isCharging = true;
        lasered = false;
        atkSpeedMultiplier = speed;

        //Check player closer to left or right side
        if (playerTransform.position.x < centerLine.position.x)
        {
            //Closer to left
            //Go right
            atkLeft = false;
            animator.AnimationChange(DragonState.MOVE, DragonDirection.LEFT);
            return playerTransform.position - leftOffset - new Vector3(0, laserTransform.localPosition.y, 0) + new Vector3(0, playerTransform.localScale.y, 0);
        }
        else
        {            
            //Closer to right
            //Go left
            atkLeft = true;
            animator.AnimationChange(DragonState.MOVE, DragonDirection.RIGHT);
            return playerTransform.position + leftOffset - new Vector3(0, laserTransform.localPosition.y, 0) + new Vector3(0, playerTransform.localScale.y, 0);
        }
    }

    public void StopAttack()
    {
        isCharging = false;
        isAttacking = false;
        GetComponent<DragonBossManager>().attacking = false;
        
        laserSpriteObj.SetActive(false);
        laserHitboxObj.SetActive(false);

        bossManager.walkSpeedMultiplier = 1;
    }
}
