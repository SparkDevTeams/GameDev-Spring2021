using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public int angle;
    public int soulsCollected;

    // Start is called before the first frame update
    void Start()
    {
        soulsCollected = 0;
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
