using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabProperties : MonoBehaviour
{
    private DoorManager door;
    //public int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponentInParent<DoorManager>();
        door.setEnemyCount(countEnemies());
    }

    private int countEnemies()
    {
        int count = 0;
        foreach(Transform transform in this.transform)
        {
            if (transform.CompareTag("EnemySpawner"))
            {
                count++;
            }
        }
        return count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
