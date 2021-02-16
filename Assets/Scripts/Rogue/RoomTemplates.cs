using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedWall;
    public List<GameObject> rooms;

    public const int MAX_ROOMS = 20;
    public bool stopGenerating;
    // Start is called before the first frame update
    void Start()
    {
        stopGenerating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(rooms.Count >= MAX_ROOMS)
        {
            stopGenerating = true;
        }
    }

    public bool generationStopped()
    {
        return stopGenerating;
    }
}
