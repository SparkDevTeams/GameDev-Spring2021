using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField]
    protected LayerMask playerLayer;

    protected override void Die(){
        //Kill enemy
        //Drop dropable if any
        //Add score if needed
        //Pool/destroy enemy
    }

    private void InflictDamage()
    {               //Add an animator event to trigger this method at the right time
        Collider2D coll = Physics2D.OverlapCircle(attackTransform.position, attackRadius, playerLayer);

         if(coll != null)
             coll.gameObject.GetComponent<HealthManager>().damage(damagePower, 0.5f);        //Works best if the player has only one collider
        
    }

}
