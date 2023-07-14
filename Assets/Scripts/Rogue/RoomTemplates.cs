using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] deadends;
    public GameObject[] itemRooms;
    public GameObject[] bossRooms;
    public GameObject[] closers;
    public GameObject[] openers;
    public GameObject[] B;
    public GameObject[] BL;
    public GameObject[] BR;
    public GameObject[] TB;
    public GameObject[] LRB;
    public GameObject[] LTB;
    public GameObject[] RTB;
    public GameObject[] L;
    public GameObject[] LR;
    public GameObject[] LRT;
    public GameObject[] R;
    public GameObject[] T;
    public GameObject[] TL;
    public GameObject[] TR;
    public GameObject closedWall;
    public List<GameObject> rooms;
    private List<GameObject> tempRooms;

    //max/min room num exclude starting room
    public int MAX_ROOMS = 20; //const
    public int MIN_ROOMS = 10; //const
    public int tblr_limit;
    public bool stopGenerating;
    public bool minReached;
    public bool itemRoomCreated;
    public bool bossRoomCreated;
    public bool shopRoomCreated;
    

    public GameObject activeRoom;

    //New room gen stuff
    int maxWidth; //max size is square
    RoomNode[] roomNodeArray;
    int roomNum;

    // Start is called before the first frame update
    void Start()
    {
        //Test
        stopGenerating = false;
        minReached = false;
        itemRoomCreated = false;
        bossRoomCreated = false;


        setClosers(false);
        setOpeners(true);
        
        //new room gen stuff
        roomNum = 0;

        maxWidth = Mathf.CeilToInt(Mathf.Sqrt(MAX_ROOMS + 1));
        if (maxWidth % 2 == 0) ++maxWidth; //make sure is odd square

        roomNodeArray = new RoomNode[maxWidth * maxWidth];

        int startIndex = (maxWidth * maxWidth - 1) / 2;
        RoomNode startNode = roomNodeArray[startIndex]; //center room is start room
        startNode = new RoomNode();
        startNode.spawned = true;
        
        while (roomNum < MAX_ROOMS)
        {            
            int nextIndex;
            RoomNode nextNode;

            //choose random direction
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0: //UP
                    if (startIndex >= maxWidth)
                    {
                        nextIndex = startIndex - maxWidth;
                        nextNode = roomNodeArray[nextIndex];

                        if (nextNode == null)
                        {
                            nextNode = new RoomNode();

                            startNode.upNode = nextNode;   
                            nextNode.downNode = startNode;

                            ++roomNum;
                        }

                        startNode = nextNode;
                        startIndex = nextIndex;                        
                    }
                    break;
                case 1: //DOWN
                    if (startIndex < maxWidth * (maxWidth - 1))
                    {
                        nextIndex = startIndex + maxWidth;
                        nextNode = roomNodeArray[nextIndex];

                        if (nextNode == null)
                        {
                            nextNode = new RoomNode();

                            startNode.downNode = nextNode;   
                            nextNode.upNode = startNode;
                            
                            ++roomNum;
                        }

                        startNode = nextNode;
                        startIndex = nextIndex;                        
                    }
                    break;
                case 2: //LEFT
                    if (startIndex % maxWidth > 0)
                    {
                        nextIndex = startIndex - 1;
                        nextNode = roomNodeArray[nextIndex];

                        if (nextNode == null)
                        {
                            nextNode = new RoomNode();

                            startNode.leftNode = nextNode;   
                            nextNode.rightNode = startNode;
                            
                            ++roomNum;
                        }

                        startNode = nextNode;
                        startIndex = nextIndex;                        
                    }
                    break;
                case 3: //RIGHT
                    if (startIndex % maxWidth < maxWidth - 1)
                    {
                        nextIndex = startIndex + 1;
                        nextNode = roomNodeArray[nextIndex];

                        if (nextNode == null)
                        {
                            nextNode = new RoomNode();

                            startNode.rightNode = nextNode;   
                            nextNode.leftNode = startNode;
                            
                            ++roomNum;
                        }

                        startNode = nextNode;
                        startIndex = nextIndex;                        
                    }
                    break;
            }
        }

        foreach (RoomNode node in roomNodeArray)
        {
            if (node != null)
            {
                //spawn
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(tblr_limit <= 0)
        //{
        //    openers[0].GetComponent<AddRoom>().setAllowed(false);
        //}

        if (rooms.Count >= MIN_ROOMS)
        {
            minReached = true;
            setClosers(true);
            setOpeners(false);
        }

        if (rooms.Count >= MAX_ROOMS)
        {
            minReached = false;
            stopGenerating = true;
        }

        //if (stopGenerating)
        //{
        //    Debug.Log("should be doing special room function");
        //    setRoomTypes();
        //    spawnRoomLayouts();

        //}

        //if (GameObject.FindGameObjectsWithTag("SpawnPoint").Length <= 0)
        //{
        //    // Do something
        //    Debug.Log(GameObject.FindGameObjectsWithTag("SpawnPoint").Length);
        //    setRoomTypes();
        //    spawnRoomLayouts();
        //}
    }

    
    

    public bool generationStopped()
    {
        return stopGenerating;
    }

    public void setActiveRoom(GameObject room)
    {
        activeRoom = room;
    }

    public GameObject getActiveRoom()
    {
        return activeRoom;
    }

    void setClosers(bool allow)
    {
        for(int i = 0; i < closers.Length; i++)
        {
            closers[i].GetComponent<AddRoom>().setAllowed(allow);
            
        }
    }

    void setOpeners(bool allow)
    {
        for (int i = 0; i < openers.Length; i++)
        {
            openers[i].GetComponent<AddRoom>().setAllowed(allow);

        }
    }

    public int getLimit()
    {
        return tblr_limit;
    }

    public bool getMin()
    {
        return minReached;
    }

    public void decrementLimit()
    {
        tblr_limit--;
    }

    public bool getItemSpawned()
    {
        return itemRoomCreated;
    }

    public void setItemSpawned(bool spawned)
    {
        itemRoomCreated = spawned;
    }

    public bool getBossSpawned()
    {
        return bossRoomCreated;
    }

    public void setBossSpawned(bool spawned)
    {
        bossRoomCreated = spawned;
    }

    public bool getShopSpawned()
    {
        return shopRoomCreated;
    }

    public void setShopSpawned(bool spawned)
    {
        shopRoomCreated = spawned;
    }

    //New room gen stuff
}
