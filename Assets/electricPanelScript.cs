using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electricPanelScript : MonoBehaviour
{ private int damageToPlayer = 18;//immediate close to death
    public int damageToEnemy = 6;

    public static bool elecFieldActive; //use to check if the field is active in the player projectyle scrript
  //COLLISIONS AND INTERACTIONS
    void OnTriggerStay2D(Collider2D col){

        if(col.CompareTag("Player")){
         Debug.Log("Crispy Bacon");        
         
             if(elecFieldActive){
             Debug.Log("THe electric talisman is active, piggy has stepped on panel");
         }else {
                Debug.Log("Damaged?");      
          col.gameObject.GetComponent<HealthManager>().damage(damageToPlayer, 0.333f); // refernece damage method from the HealthManager, when it checks the player collider 
          //how to make stun first? to make it look like they got electrocuted 
         }
           
            

        } 

        if (col.CompareTag("Enemy")){
            Debug.Log("Enemy got electrouted, F");
             col.gameObject.GetComponent<EnemyManager>().Damage(damageToEnemy);
        }   


 }
void Update(){
  elecFieldActive = PlayerProjectyle.fieldActive;

}
}
