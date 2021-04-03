using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = cameraPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCameraPosition(Transform cameraPoint)
    {
        this.transform.position = cameraPoint.transform.position;
    }
    
}
