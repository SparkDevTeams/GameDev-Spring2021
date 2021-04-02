using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public int angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame        
    void Update()
    {
        if(Input.GetAxisRaw("Vertical") == 1){
            
            angle = 90;
        }
        if(Input.GetAxisRaw("Vertical") == -1){
            
            angle = 270;
        }
        if(Input.GetAxisRaw("Horizontal") == 1){
            
            angle = 0;
        }
        if(Input.GetAxisRaw("Horizontal") == -1){
            
            angle = 180;
        }
    }
}
