using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Transform cameraPoint;
    private CameraManager camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Camera change!");
            camera.setCameraPosition(cameraPoint);
        }
        
    }
}
