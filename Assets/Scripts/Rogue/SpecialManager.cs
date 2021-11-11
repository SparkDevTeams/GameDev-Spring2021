using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialManager : MonoBehaviour
{
    private bool itemRoomSpawned;
    private bool bossRoomSpawned;
    // Start is called before the first frame update
    void Start()
    {
        itemRoomSpawned = false;
        bossRoomSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setItemRoom(bool spawned)
    {
        itemRoomSpawned = spawned;
    }

    public void setBossRoom(bool spawned)
    {
        bossRoomSpawned = spawned;
    }

    public bool getItemRoom()
    {
        return itemRoomSpawned;
    }

    public bool getBossRoom()
    {
        return bossRoomSpawned;
    }
}
