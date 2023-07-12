using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossFireballAttack : MonoBehaviour
{
    public float attackTime; //Time before dragon can do other attacks
    private int attacking = 0; //0 for not started, 1 for animation stuff, 2 for raining, 3 for done
    public float delayTime; //Time before fireballs start raining down
    private float delayTimer = 0;
    public float upFireballTime;
    bool upFireballed = false;
    public float fireballTime;
    private float fireballTimer = 0;
    public Transform fireballOrigin;

    private Transform playerTarget;

    public GameObject fireballPrefab;
    private DragonBossAnimator animator;
    bool stopped = false;

    // Start is called before the first frame update
    void Start()
    {        
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<DragonBossAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking == 1)
        {
            delayTimer += Time.deltaTime;

            if (delayTimer >= upFireballTime && !upFireballed)
            {
                upFireballed = true;
                
                Shoot(false, Vector3.zero);
            }

            if (delayTimer >= attackTime && !stopped)
            {
                StopAttack();
            }

            if (delayTimer >= delayTime)
            {
                attacking = 2;
                fireballTimer = 0;
            }
        }
        else if (attacking == 2)
        {
            fireballTimer += Time.deltaTime;
            if (fireballTimer >= fireballTime)
            {
                Vector3 targetPos = playerTarget.position;

                //Shoot fireball
                Shoot(true, targetPos, 4);

                fireballTimer = 0;
            }
        }
    }



    public void StartAttack()
    {
        animator.AnimationChange(DragonState.FIREBALL, DragonDirection.LEFT, 1, 0, 0);

        attacking = 1;
        delayTimer = 0;
        stopped = false;
    }

    public void StopAttack()
    {   
        stopped = true;
        GetComponent<DragonBossManager>().attacking = false;
    }

    public void StopFireballs()
    {
        attacking = 3;
    }

    void Shoot(bool falling, Vector3 TargetPos, float time = 1)
    {
        GameObject fireball;
        FireballBehaviour behaviour;
        if (falling)
        {
            fireball = Instantiate(fireballPrefab, new Vector3(TargetPos.x, -250, TargetPos.z), Quaternion.identity); //hardcoded based on position of arena and camera
            behaviour = fireball.GetComponent<FireballBehaviour>();

            behaviour.falling = true;
            behaviour.targetPosY = TargetPos.y;
            behaviour.speed = Mathf.Abs(-250 - TargetPos.y) / time;
            behaviour.time = time;
            behaviour.haveShadow = true;
        }
        else
        {
            fireball = Instantiate(fireballPrefab, fireballOrigin.position, Quaternion.Euler(0, 0, 180));
            fireball.transform.localScale *= 2;
            behaviour = fireball.GetComponent<FireballBehaviour>();
            
            behaviour.falling = false;
            behaviour.targetPosY = -250; //hardcoded based on position of arena and camera
            behaviour.speed = 25;
            behaviour.haveShadow = false;
        }
    }
}
