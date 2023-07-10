using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    RoomManager room;
    DoorManager door;

    bool enteredBefore;
    public GameObject[] enemyPrefabs;
    public Transform[] enemySpawnPoints;
    PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        room = GetComponentInParent<RoomManager>();
        door = GetComponentInParent<DoorManager>();
        enteredBefore = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (enteredBefore == false)
            {
                playerStats = collision.gameObject.GetComponent<PlayerStats>();
                SpawnEnemyPrefabs();
                enteredBefore = true;
                if (playerStats.currentLevel > 0)
                {
                    SpawnAdditionalEnemies();
                }
            }
            room.setPlayerInside(true);
            door.setClosed(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            room.setPlayerInside(false);
        }
        
    }

    void SpawnEnemyPrefabs()
    {
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemyIndex], enemySpawnPoints[i].position, enemySpawnPoints[i].rotation);
        }
    }

    void SpawnAdditionalEnemies()
    {
        int additionalEnemies = playerStats.currentLevel;
        if (additionalEnemies > 4)
        {
            additionalEnemies = 4;
        }
        for (int i = 0; i <= additionalEnemies; i++)
        {
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemyIndex], enemySpawnPoints[i].position, enemySpawnPoints[i].rotation);
        }
    }

}
