using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
   

    
    protected override void Die(){
        //Kill enemy
        //Drop dropable if any
        //Add score if needed
        //Pool/destroy enemy
    }

    private void InflictDamage(){               //Add an animator event to trigger this method at the right time
        Collider2D[] colls = Physics2D.OverlapCircleAll(attackTransform.position, attackRadius);
        foreach(Collider2D coll in colls){
            if(coll.GetComponent<HealthManager>() != null)
                coll.GetComponent<HealthManager>().damage(damagePower);        //Works best if the player has only one collider
        }
    }

}
