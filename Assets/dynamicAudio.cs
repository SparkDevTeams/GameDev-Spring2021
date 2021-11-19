using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class dynamicAudio : MonoBehaviour
{


    public AudioMixerSnapshot normal;
    public AudioMixerSnapshot nearDeath;
    private HealthManager healthScript;
    int currenthealth;
    // Start is called before the first frame update
    void Start()
    {
        healthScript = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currenthealth = healthScript.getHealth();
        lowPassForHealth();
    }

    void lowPassForHealth(){
        if(currenthealth <= 5){
            Debug.Log("working health music");
            nearDeath.TransitionTo(0.5f);
        } else {
             Debug.Log("normal health music");
            normal.TransitionTo(0.5f);
        }


    }


}
