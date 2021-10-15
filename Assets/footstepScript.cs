using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepScript : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource audioSource;
    public string mode = ""; 
    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
      
    }

    // Update is called once per frame
    void Update()
    {
          mode = GetComponent<move>().Mode;
        moveCheck();
        FP();
    }



    void moveCheck(){
        if(rb.velocity.x != 0 || rb.velocity.y !=0){
            if(mode != "roll"){
            isMoving = true;
            }
        } else {
            isMoving = false;
        }
    }

    void FP(){
        if(isMoving){
            if(!audioSource.isPlaying)
            audioSource.Play();
        } else {
            audioSource.Stop();
        }
    }

    }

