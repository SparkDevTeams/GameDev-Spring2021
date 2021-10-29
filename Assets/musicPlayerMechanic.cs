using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayerMechanic : MonoBehaviour //notice the class name
{
    public AudioClip firstAudioClip; 
    private bool wasHit = false;
    private bool m_Play = false;
    private bool m_ToggleChange;
    private bool playerLeave = false;
    AudioSource m_MyAudioSource;
    public AudioSource  radioNoise;
    public AudioClip[] audioClips;
    private int randNum;
    // Start is called before the first frame update
    void Start()
    {
        randNum = Random.Range(0, 3);
        radioNoise.Play();
       // m_MyAudioSource.PlayOneShot(firstAudioClip, 0.3f);
      m_MyAudioSource  = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        musicPlayer();
    }

                
    public void firstHit(){
        if(wasHit == false){
            wasHit = true; 
            m_Play = true;
            m_ToggleChange = true;
            //make it play here? 
        }

    }

    public void musicPlayer(){
            if (wasHit == true){
                 Debug.Log("Now playing the music of the souls!");
          //  m_MyAudioSource.Play();
         
          
            }else  {
           // Debug.Log("called music player, it was not hit tho");
            }

            if(m_Play == true && m_ToggleChange == true){ // if you hit jukebox it will play 
                radioNoise.Stop();
                m_MyAudioSource.clip = audioClips[randNum];
                m_MyAudioSource.PlayDelayed(1.5f);
                m_ToggleChange = false;
            }

            if(m_Play == false && m_ToggleChange == true){
                m_MyAudioSource.Stop();
                m_ToggleChange = false;
            }

           


    }


  void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
           // Debug.Log("You hit the jukebox D: ");
        }

    }
    // void OnTriggerExit2D(Collider2D col){//if player exits after they open it, the jukebox will never play again
    //     if(col.tag == "Player"){
    //         playerLeave = true;
    //         Debug.Log("player left the jukebox room");
    //     }

    // }
   public void playerLeaveMethod(){
    
        playerLeave = true;
    }

    public void playerEnterMethod(){

        playerLeave = false;
    }

}
