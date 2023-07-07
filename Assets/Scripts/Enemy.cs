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

}
