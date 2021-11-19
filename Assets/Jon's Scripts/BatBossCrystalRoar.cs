using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossCrystalRoar : MonoBehaviour
{
    const int EVENT_ID = 11;

    private BatBossAnimator animator;
    [SerializeField]
    private float totalStartUpTime = 1.0f;
    [SerializeField]
    private float totalRoarTime = 2.0f;
    [SerializeField]
    private float totalCooldownTime = 1.0f;
    private float cooldownTimer = 0.0f;
    private float startUpTimer = 0.0f;
    private float attackTimer = 0.0f;
    private bool roaring = false;
    private bool startingUp = false;
    private bool attacking = false;

    void Start()
    {
        animator = GetComponent<BatBossAnimator>();
        cooldownTimer = 0.0f;
        startUpTimer = 0.0f;
        attackTimer = 0.0f;
        roaring = false;
        startingUp = false;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (roaring) 
        {
            if (startingUp)
            {
                startUpTimer -= Time.deltaTime;

                if (startUpTimer <= 0.0f)
                {
                    attackTimer = totalRoarTime;
                    attacking = true;
                    startingUp = false;
                }
            }
            else if (attacking)
            {
                attackTimer -= Time.deltaTime;

                if (attackTimer <= 0.0f)
                {
                    cooldownTimer = totalCooldownTime;
                    attacking = false;
                }
            }
            else
            {
                cooldownTimer -= Time.deltaTime;

                if (cooldownTimer <= 0.0f)
                {
                    roaring = false;
                }
            }
        }
    }

    public void Roar() 
    {
        roaring = true;
        startUpTimer = totalStartUpTime;
        startingUp = true;
        animator.Roar(totalStartUpTime, totalRoarTime, totalCooldownTime);
        EventSystem.eventController.CrystalTrigger(11);
    }

    public bool IsRoaring() 
    {
        return roaring;
    }
}
