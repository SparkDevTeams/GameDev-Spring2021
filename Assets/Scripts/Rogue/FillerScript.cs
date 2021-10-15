using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillerScript : MonoBehaviour
{
    public Transform blockPoint;
    public GameObject doorBlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Colliding with: "+collision.name);
        if (collision.name == "Walls")
        {
            Debug.Log("Spawn the block!");
            Instantiate(doorBlock, blockPoint.position, blockPoint.rotation);
        }
    }
}
