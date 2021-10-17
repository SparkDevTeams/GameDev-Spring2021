using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public float maxVolume = 1f; 
    public float minVolume = 0f; 
    private AudioSource source; 
    public AudioClip clip;

    public float fadeInDuration = 2f; 
    public float fadeOutDuration = 2f; 
    
    
    private IEnumerator fadeIn;
    private IEnumerator fadeOut;

    public static MusicManager instance; 

    private void Awake(){
            if(!instance)
                instance = this;
            source = GetComponent<AudioSource>();

    }


    public void StartMusic(){
        if(fadeOut != null)
            StopCoroutine(fadeOut);


        source.clip = clip; 
        source.Play();
        fadeIn = FadeIn(source, fadeInDuration, maxVolume);
        StartCoroutine(fadeIn);

    }

     public void StopMusic(){
         fadeOut = FadeOut(source, fadeOutDuration, minVolume);
         if(source.isPlaying){
             StopCoroutine(fadeIn);
             StartCoroutine(fadeOut);
         }
        
    }

    IEnumerator FadeIn(AudioSource aSource, float duration, float targetVolume){
            float timer = 0f; 
            float currentVolume = aSource.volume; 
            float targetValue = Mathf.Clamp(targetVolume, minVolume, maxVolume); 

            while(timer < duration){
                timer += Time.deltaTime;
                var newVolume = Mathf.Lerp(currentVolume, targetVolume, timer / duration);
                aSource.volume = newVolume;
                 yield return null;

            }

    }

    IEnumerator FadeOut(AudioSource aSource, float duration, float targetVolume){
            float timer = 0f; 
            float currentVolume = aSource.volume; 
            float targetValue = Mathf.Clamp(targetVolume, minVolume, maxVolume); 

            while(aSource.volume > 0){
                timer += Time.deltaTime;
                var newVolume = Mathf.Lerp(currentVolume, targetVolume, timer / duration);
                aSource.volume = newVolume;
                 yield return null;

            }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
