using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 = bottom door, 2 = top door, 3 = left door, 4 = right door

    private RoomTemplates templates;
    private int rand;
    public int deadends;
    public bool spawned = false;
    private float waitTime = 10f;


    // Start is called before the first frame update
    void Start()
    {
        deadends = 0;
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
                    do
                    {
                        rand = Random.Range(0, templates.bottomRooms.Length);
                        
                    } while (!templates.bottomRooms[rand].GetComponent<AddRoom>().getAllowed());

                    

                    if (!templates.getItemSpawned() && templates.bottomRooms[rand].GetComponent<AddRoom>().isDeadend)
                    {
                        Instantiate(templates.itemRooms[0], transform.position, templates.itemRooms[0].transform.rotation);
                        templates.setItemSpawned(true);
                        Debug.Log("Item room spawned new");
                    }
                    else
                    {
                        Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                    }


                    break;
                case 2:
                    do
                    {
                        rand = Random.Range(0, templates.topRooms.Length);
                    } while (!templates.topRooms[rand].GetComponent<AddRoom>().getAllowed());


                    

                    if (!templates.getItemSpawned() && templates.topRooms[rand].GetComponent<AddRoom>().isDeadend)
                    {
                        Instantiate(templates.itemRooms[1], transform.position, templates.itemRooms[1].transform.rotation);
                        templates.setItemSpawned(true);
                        Debug.Log("Item room spawned new");
                    }
                    else
                    {
                        Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    }

                    break;
                case 3:
                    do
                    {
                        rand = Random.Range(0, templates.leftRooms.Length);
                    } while (!templates.leftRooms[rand].GetComponent<AddRoom>().getAllowed());

                   
                    if (!templates.getItemSpawned() && templates.leftRooms[rand].GetComponent<AddRoom>().isDeadend)
                    {
                        Instantiate(templates.itemRooms[2], transform.position, templates.itemRooms[2].transform.rotation);
                        templates.setItemSpawned(true);
                        Debug.Log("Item room spawned new");
                    }
                    else
                    {
                        Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    }
                    break;
                case 4:
                    do
                    {
                        rand = Random.Range(0, templates.rightRooms.Length);
                    } while (!templates.rightRooms[rand].GetComponent<AddRoom>().getAllowed());

                    
                    if (!templates.getItemSpawned() && templates.rightRooms[rand].GetComponent<AddRoom>().isDeadend)
                    {
                        Instantiate(templates.itemRooms[3], transform.position, templates.itemRooms[3].transform.rotation);
                        templates.setItemSpawned(true);
                        Debug.Log("Item room spawned new");
                    }
                    else
                    {
                        Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    }
                    break;

            }
            spawned = true;
        }

        if(!spawned && templates.generationStopped())
        {
            switch (openingDirection)
            {
                case 1:
                    if (templates.getBossSpawned() && templates.getItemSpawned())
                    {
                        Instantiate(templates.deadends[0], transform.position, templates.deadends[0].transform.rotation);
                        Debug.Log("Deadend spawned");
                        break;
                    }
                    else if(templates.getBossSpawned() && !templates.getItemSpawned())
                    {
                        Instantiate(templates.itemRooms[0], transform.position, templates.itemRooms[0].transform.rotation);
                        templates.setItemSpawned(true);
                        Debug.Log("Item room spawned old");
                        break;
                    }
                    else
                    {
                        Instantiate(templates.bossRooms[0], transform.position, templates.bossRooms[0].transform.rotation);
                        templates.setBossSpawned(true);
                        Debug.Log("Boss room spawned");
                        break;
                    }
                    
                case 2:
                    if (templates.getBossSpawned() && templates.getItemSpawned())
                    {
                        Instantiate(templates.deadends[1], transform.position, templates.deadends[1].transform.rotation);
                        Debug.Log("Deadend spawned");
                        break;
                    }
                    else if (templates.getBossSpawned() && !templates.getItemSpawned())
                    {
                        Instantiate(templates.itemRooms[1], transform.position, templates.itemRooms[1].transform.rotation);
                        templates.setItemSpawned(true);
                        Debug.Log("Item room spawned old");
                        break;
                    }
                    else
                    {
                        Instantiate(templates.bossRooms[1], transform.position, templates.bossRooms[1].transform.rotation);
                        templates.setBossSpawned(true);
                        Debug.Log("Boss room spawned");
                        break;
                    }

                case 3:
                    if (templates.getBossSpawned() && templates.getItemSpawned())
                    {
                        Instantiate(templates.deadends[2], transform.position, templates.deadends[2].transform.rotation);
                        Debug.Log("Deadend spawned");
                        break;
                    }
                    else if (templates.getBossSpawned() && !templates.getItemSpawned())
                    {
                        Instantiate(templates.itemRooms[2], transform.position, templates.itemRooms[2].transform.rotation);
                        templates.setItemSpawned(true);
                        Debug.Log("Item room spawned old");
                        break;
                    }
                    else
                    {
                        Instantiate(templates.bossRooms[2], transform.position, templates.bossRooms[2].transform.rotation);
                        templates.setBossSpawned(true);
                        Debug.Log("Boss room spawned");
                        break;
                    }

                case 4:
                    if (templates.getBossSpawned() && templates.getItemSpawned())
                    {
                        Instantiate(templates.deadends[3], transform.position, templates.deadends[3].transform.rotation);
                        Debug.Log("Deadend spawned");
                        break;
                    }
                    else if (templates.getBossSpawned() && !templates.getItemSpawned())
                    {
                        Instantiate(templates.itemRooms[3], transform.position, templates.itemRooms[3].transform.rotation);
                        templates.setItemSpawned(true);
                        Debug.Log("Item room spawned old");
                        break;
                    }
                    else
                    {
                        Instantiate(templates.bossRooms[3], transform.position, templates.bossRooms[3].transform.rotation);
                        templates.setBossSpawned(true);
                        Debug.Log("Boss room spawned");
                        break;
                    }

            }
            
        }
        
    }

    void checkAllow(GameObject room)
    {

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
