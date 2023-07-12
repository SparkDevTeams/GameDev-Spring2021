using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossLandingAttack : MonoBehaviour
{
    public int landingDamage;
    public float dmgTime = 0.25f;
    public float attackTime;
    public float attackTimer = 0;
    bool attacking = false;
    bool hitPlayer = false;
    public SpriteRenderer sprite;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking) //change to check animation time finished (later)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackTime)
            {
                EndAttack();
            }
        }
    }

    void OnTriggerStay2D (Collider2D hitInfo)
    {
        if(hitInfo.tag == "Player")
        {
            if (attacking && !hitPlayer && attackTimer < dmgTime)
            {
                hitInfo.GetComponent<HealthManager>().damage(landingDamage,0.25f);
                hitPlayer = true;
            }            
        }
    }

    public void StartAttack()
    {
        attacking = true;
        hitPlayer = false;
        attackTimer = 0;
        sprite.enabled = true;
        animator.Play("DragonLandingSmoke", sprite.sortingLayerID, 0);
    }

    private void EndAttack()
    {
        attacking = false;
        sprite.enabled = false;
    }
}
