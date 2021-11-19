using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private TeleportManager teleportManager;
    void Start()
    {
        teleportManager = FindObjectOfType<TeleportManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.position = teleportManager.getBossRoom().position;
            teleportManager.teleported(true);
        }
    }
}
