using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class musicManager : MonoBehaviour
{

    public AudioMixerSnapshot calmState;
     public AudioMixerSnapshot enemyState;
     bool enemyNear = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        musicChanger();
       
        Debug.Log("Enemy near = " + enemyNear);
    }
    void OnTriggerStay2D(Collider2D col){
    
     if (col.CompareTag("Enemy")){
            Debug.Log("There is an enemy inside the music checker!!");
             enemyNear = true; 
         }
}

void OnTriggerExit2D(Collider2D col){
    
     if (col.CompareTag("Enemy")){
            Debug.Log("no more enemies");
             enemyNear = false; 
         }
}




void musicChanger(){
    if(enemyNear == false){ 
    
        calmState.TransitionTo(3f);


    }
    
    if(enemyNear == true){
        enemyState.TransitionTo(3f);
    }

}


}
