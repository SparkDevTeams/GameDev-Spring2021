using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    
    protected override void Die(){
        // Kill player
    }

    private void InflictDamage(){               //Add an animator event to trigger this method at the right time
        Collider2D[] colls = Physics2D.OverlapCircleAll(attackTransform.position, attackRadius);
        foreach(Collider2D coll in colls){
            coll.GetComponent<Enemy>().TakeDamage(damagePower);         //Works best if the enemy has only one collider
        }
    }

}
