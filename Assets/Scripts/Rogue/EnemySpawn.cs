using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    private RoomManager room;
    private bool spawned;

    public GameObject enemy;
    void Start()
    {
        room = GetComponentInParent<RoomManager>();
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Spawns an enemy in a given spawnpoint once the player steps inside it's room
        if(room.getPlayerInside() && !spawned)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            //enemy.transform.tag = "Enemy";
            spawned = true;
        }
    }
}
