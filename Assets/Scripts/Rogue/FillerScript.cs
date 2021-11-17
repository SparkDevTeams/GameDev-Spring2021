using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillerScript : MonoBehaviour
{
    public Transform blockPoint;
    public GameObject doorBlock;

    bool spawnedBlocks;
    // Start is called before the first frame update
    void Start()
    {
        spawnedBlocks = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Colliding with: "+collision.name);
        if (collision.name == "Walls" && !spawnedBlocks)
        {
            //Debug.Log("Spawn the block!");
            Instantiate(doorBlock, blockPoint.position, blockPoint.rotation);
            spawnedBlocks = true;
        }
    }

    
}
