using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioHealthEffect : MonoBehaviour
{

    //max health is 20
    public AudioMixerSnapshot normalHealthEffect;
     public AudioMixerSnapshot lowHealthEffect;
     
    
   private int currentHealth = 0;
    bool lowHealth = false;
    public HealthManager myPlayer;
    public float transitionTime = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
       myPlayer = FindObjectOfType<HealthManager>();
    }
    // Update is called once per frame
    void Update()
    {
        currentHealth = myPlayer.getHealth();
        Debug.Log("Health = " + currentHealth);

        healthChecker();
        lowPass();
    }


     void healthChecker(){
            if(currentHealth < 5) {//less than 25 percent? 
                lowHealth = true;
            } else {
                lowHealth = false;
            }
            
    }
     void lowPass(){
            if(lowHealth == false){
                normalHealthEffect.TransitionTo(transitionTime);
            } else {
                lowHealthEffect.TransitionTo(transitionTime);
            }

    }



    
}
