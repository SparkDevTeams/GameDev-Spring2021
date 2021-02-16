using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform currentCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCheckpoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public Transform getTransform()
    {
        return currentCheckpoint;
    }
}
