using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    DoorManager door;
    
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponentInParent<DoorManager>();
        setActive(false);

        //this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (door.getClosed())
        {
            setActive(true);
            Debug.Log("Close");
        }
        else
        {
            setActive(false);
            Debug.Log("Open");
        }
    }

    void setActive(bool active)
    {
        this.GetComponent<BoxCollider2D>().enabled = active;
        this.GetComponent<SpriteRenderer>().enabled = active;

    }
}
