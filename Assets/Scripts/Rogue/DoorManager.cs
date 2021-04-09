using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private RoomManager room;
    public bool isClosed;
    public int enemyCount;

    //public GameObject doors;
    // Start is called before the first frame update
    void Start()
    {
        room = GetComponentInParent<RoomManager>();
        //doors.SetActive(false);
        isClosed = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isClosed)
        //{
        //    //this.enabled = true;
        //    doors.SetActive(true);
        //}
        //else
        //{
        //    //this.enabled = false;
        //    doors.SetActive(false);
        //}
        if(enemyCount <= 0)
        {
            setClosed(false);
        }
    }

    public void setClosed(bool closed)
    {
        isClosed = closed;
        //Debug.Log("Close door");
    }

    public bool getClosed()
    {
        return isClosed;
    }

    public void killEnemy()
    {
        enemyCount--;
    }
}


