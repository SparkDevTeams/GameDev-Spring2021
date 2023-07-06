using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossLaserAttack : MonoBehaviour
{
    [SerializeField]
    private Transform laserTransform;    
    [SerializeField]
    private GameObject laserSpriteObj;
    [SerializeField]
    private GameObject laserHitboxObj;
    [SerializeField]
    private float totalChargeTime;
    private float maxScaleY = 2;
    [SerializeField]
    private float totalAttackTime;
    private float chargeTimer = 0;
    private float attackTimer = 0;
    private bool isCharging = false;
    private bool isAttacking = false;
    
    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        laserHitboxObj.transform.localPosition = new Vector3(laserHitboxObj.transform.localPosition.x, 0, laserHitboxObj.transform.localPosition.z);

        if (isCharging)
        {
            if (chargeTimer < totalChargeTime)
            {
                chargeTimer += Time.deltaTime;
                //Scale up based on charge time
                float scaleY = maxScaleY * (chargeTimer / totalChargeTime);
                if (scaleY <= 0.1f)
                {
                    scaleY = 0.1f;
                }
                laserTransform.localScale = new Vector3(laserTransform.localScale.x, scaleY, laserTransform.localScale.z);
            }
            else
            {
                laserTransform.localScale = new Vector3(laserTransform.localScale.x, maxScaleY, laserTransform.localScale.z);
                //start attack
                isAttacking = true;
                isCharging = false;

                laserHitboxObj.SetActive(true);
            }
        }
        else if (isAttacking)
        {
            if (attackTimer < totalAttackTime)
            {
                attackTimer += Time.deltaTime;
            }
            else
            {
                //end attack
                StopAttack();
            }
        }        
    }

    public void StartAttack() //charge
    {
        attackTimer = 0;
        chargeTimer = 0;
        isCharging = true;

        laserSpriteObj.SetActive(true);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, 0.1f, laserTransform.localScale.z);
    }

    public void StopAttack()
    {
        isCharging = false;
        isAttacking = false;
        GetComponent<DragonBossManager>().attacking = false;
        
        laserSpriteObj.SetActive(false);
        laserHitboxObj.SetActive(false);
    }
}
