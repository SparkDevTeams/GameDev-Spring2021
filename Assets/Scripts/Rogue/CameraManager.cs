using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform cameraPosition;
    [SerializeField] public Transform bossCamera;
    private TeleportManager teleport;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        teleport = FindObjectOfType<TeleportManager>();
        this.transform.position = cameraPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (teleport.getTeleported())
        {
            //this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
            this.transform.position = new Vector3(bossCamera.transform.position.x, bossCamera.transform.position.y, this.transform.position.z);
            GetComponent<Camera>().orthographicSize = 13;
        }
    }

    public void setCameraPosition(Transform cameraPoint)
    {
        this.transform.position = cameraPoint.transform.position;
    }
    
}
