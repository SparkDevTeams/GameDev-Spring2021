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
        if(room.getPlayerInside() && !spawned)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            //enemy.transform.tag = "Enemy";
            spawned = true;
        }
    }
}
