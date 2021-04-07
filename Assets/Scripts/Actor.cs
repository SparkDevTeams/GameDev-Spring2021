using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int maxHealth;
    public Transform attackTransform;
    public float attackRadius;
    public int damagePower;

    private int health;

    void Start(){
        health = maxHealth;
    }

    
    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            Die();
        }
        if(GetComponent<Enemy>() != null){                          //This if block is just to help you understand what's going on
            Debug.Log("Enemy damaged, " + health + "HP left");
        } else if(GetComponent<Player>() != null){
            Debug.Log("Player damaged, " + health + "HP left");
        }
    }

    protected virtual void Die(){
        
    }

    private void OnDrawGizmos() {               //Red Gizmo for attacking radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTransform.position, attackRadius);
    }

}
