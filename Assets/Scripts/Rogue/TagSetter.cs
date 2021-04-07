using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagSetter : MonoBehaviour
{
    public string tag;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
