using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossBreathAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject fireBreathObj;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float totalAttackTime;
    private float attackTimer = 0;
    private bool isAttacking = false;
    
    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            if (attackTimer > 0)
            {

            }
            else
            {
                //end attack
                StopAttack();
            }
        }        
    }

    public void StartAttack()
    {
        attackTimer = totalAttackTime;
        isAttacking = true;
    }

    public void StopAttack()
    {
        attackTimer = 0;
        isAttacking = false;
        GetComponent<DragonBossManager>().attacking = false;
    }
}
