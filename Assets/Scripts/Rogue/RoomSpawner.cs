using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 = bottom door, 2 = top door, 3 = left door, 4 = right door

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;
    private float waitTime = 10f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.35f);
    }

    // Update is called once per frame
    void Spawn()
    {

        if (!spawned && !templates.generationStopped())
        {
            switch (openingDirection)
            {
                case 1:
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                    break;
                case 2:
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    break;
                case 3:
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    break;
                case 4:
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    break;

            }
            spawned = true;
        }

        if(!spawned && templates.generationStopped())
        {
            Instantiate(templates.closedWall, transform.position, templates.closedWall.transform.rotation);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpawnPoint"))
        {
            if(collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedWall, transform.position, templates.closedWall.transform.rotation);
                Destroy(gameObject);
            }
            spawned = true;
            
        }
    }
}
