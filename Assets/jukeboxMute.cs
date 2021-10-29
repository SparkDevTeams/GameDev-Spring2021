using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class jukeboxMute : MonoBehaviour
{

    public AudioMixerSnapshot mute; 
    public AudioMixerSnapshot normal;
    public bool inJukeboxRoom = false;
    private musicPlayerMechanic mpm;
    
    // Start is called before the first frame update
    void Start()
    {
       mpm = FindObjectOfType<musicPlayerMechanic>();
    }

    // Update is called once per frame
    void Update()
    {
        muteJukebox();
    }
    void OnTriggerEnter2D(Collider2D col){

        if(col.CompareTag("Player")){
            //Debug.Log("jukebox collide");
            inJukeboxRoom = true;
        }

    }
 void OnTriggerExit2D(Collider2D col){
    if(col.CompareTag("Player")){
      //  Debug.Log("Did piggy leave? ");
            inJukeboxRoom = false;
          
    }
}


    void muteJukebox(){
        if(inJukeboxRoom == true){
         //   Debug.Log("mute transition");
            mute.TransitionTo(0.5f);
        } 
        if(inJukeboxRoom == false)  {
         //        Debug.Log("normal transition");
        
              normal.TransitionTo(0.5f);
              mpm.playerLeaveMethod();
        }
    }
    



}
