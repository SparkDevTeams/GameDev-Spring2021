using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toxicGasScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

  


     public int totalGasDamage = 3;
    public int totalGasDuration = 3;
    public float totalTime = 0;
   
    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerStay2D(Collider2D col){

         totalTime += Time.deltaTime;

         if(col.CompareTag("Player")){
                if(totalTime > 0.25f){ //while player is moving in gas, it will damage player. change to automatic damage?
                     col.gameObject.GetComponent<HealthManager>().damage(1, 0.25f);
                     
                     totalTime = 0;
                }
         }

     }
}
