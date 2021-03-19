using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    RoomManager room;
    // Start is called before the first frame update
    void Start()
    {
        room = GetComponentInParent<RoomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        room.setPlayerInside(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        room.setPlayerInside(false);
    }
}
