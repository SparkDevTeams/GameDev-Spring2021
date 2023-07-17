using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject B;
    public GameObject BL;
    public GameObject BLR;
    public GameObject BLRT;
    public GameObject BLT;
    public GameObject BR;
    public GameObject BRT;
    public GameObject BT;
    public GameObject L;
    public GameObject LR;
    public GameObject LRT;
    public GameObject LT;
    public GameObject R;
    public GameObject RT;
    public GameObject T;

    //max room num exclude starting room
    public int MAX_ROOMS = 20; //const
    public bool stopGenerating;
    public bool itemRoomCreated;
    public bool bossRoomCreated;
    public bool shopRoomCreated;
    

    public GameObject activeRoom;

    //New room gen stuff
    List<GameObject> rooms = new List<GameObject>();
    int maxWidth; //max size is square
    RoomNode[] roomNodeArray;
    int roomNum;
    int currentDir = -1;
    public Transform startRoomTransform;
    public Transform downSpawnPoint, rightSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        //new room gen stuff
        stopGenerating = false;

        maxWidth = Mathf.CeilToInt(Mathf.Sqrt(MAX_ROOMS + 1));
        if (maxWidth % 2 == 0) ++maxWidth; //make sure is odd square
        else maxWidth += 2; //make more space

        roomNodeArray = new RoomNode[maxWidth * maxWidth];
        for (int i = 0; i < roomNodeArray.Length; ++i)
        {
            roomNodeArray[i] = new RoomNode();
        }

        int middleIndex = (maxWidth * maxWidth - 1) / 2;

        RoomNode startMiddleNode = roomNodeArray[middleIndex];
        startMiddleNode.spawned = true;
        startMiddleNode.filled = true;     

        RoomNode startNode = startMiddleNode; //center room is start room
        int startIndex = middleIndex;
        
        roomNum = 0;

        int randTimes = 0;
        int rand = Random.Range(0, 4);
        
        while (roomNum < MAX_ROOMS)
        {
            --randTimes;
            if (randTimes <= 0)
            {
                randTimes = Random.Range(1, 4);
                rand = Random.Range(0, 4);
            }   

            if (roomNum == 0 && currentDir < 0)
            {
                startIndex = middleIndex;
                startNode = startMiddleNode;
                randTimes = 2;
                rand = 0;
                currentDir = 0;
            }
            else if (roomNum == MAX_ROOMS / 4 && currentDir < 1)
            {
                startIndex = middleIndex;
                startNode = startMiddleNode;
                randTimes = 2;
                rand = 1;
                currentDir = 1;
            }
            else if (roomNum == MAX_ROOMS / 2 && currentDir < 2)
            {
                startIndex = middleIndex;
                startNode = startMiddleNode;
                randTimes = 2;
                rand = 2;
                currentDir = 2;
            }
            else if (roomNum == MAX_ROOMS / 4 * 3 && currentDir < 3)
            {
                startIndex = middleIndex;
                startNode = startMiddleNode;
                randTimes = 2;
                rand = 3;
                currentDir = 3;
            }

            int nextIndex;
            RoomNode nextNode;         

            //choose random direction
            switch (rand)
            {
                case 0: //UP
                    if (startIndex >= maxWidth)
                    {
                        nextIndex = startIndex - maxWidth;
                        nextNode = roomNodeArray[nextIndex];

                        startNode.upNode = nextNode;   
                        nextNode.downNode = startNode;

                        if (!nextNode.filled)
                        {
                            nextNode.filled = true;

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

                        startNode.downNode = nextNode;   
                        nextNode.upNode = startNode;

                        if (!nextNode.filled)
                        {
                            nextNode.filled = true;
                            
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
                            
                        startNode.leftNode = nextNode;   
                        nextNode.rightNode = startNode;

                        if (!nextNode.filled)
                        {
                            nextNode.filled = true;
                            
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
                            
                        startNode.rightNode = nextNode;   
                        nextNode.leftNode = startNode;

                        if (!nextNode.filled)
                        {
                            nextNode.filled = true;
                            
                            ++roomNum;
                        }

                        startNode = nextNode;
                        startIndex = nextIndex;                        
                    }
                    break;
            }
        }

        //show map in debug
        string mapStr = "";
        for (int i = 0; i < roomNodeArray.Length; ++i)
        {
            RoomNode currNode = roomNodeArray[i];

            if (i % maxWidth == 0) mapStr += '\n';

            if (i == middleIndex) mapStr += "P";
            else if (currNode.filled) mapStr += 'X';
            else mapStr += 'O';
        }
        Debug.Log("Visualizing map:\n" + mapStr);

        //Spawn map

        //Get coords to spawn
        //Distances in positive direction
        Vector3 vertDist = downSpawnPoint.localPosition;
        Vector3 horizontalDist = rightSpawnPoint.localPosition;
        Vector3 startPos = startRoomTransform.position - vertDist * (maxWidth - 1 ) / 2 - horizontalDist * (maxWidth - 1) / 2;

        for (int i = 0; i < roomNodeArray.Length; ++i)
        {
            RoomNode currNode = roomNodeArray[i];

            if (currNode.filled && i != middleIndex)
            {
                //find spawn pos
                Vector3 spawnPos = startPos + vertDist * (i / maxWidth) + horizontalDist * (i % maxWidth);
                //find type of room to spawn (which openings)
                string roomName = ""; //order shld be BLRT
                if (currNode.downNode != null)
                {
                    roomName += 'B';
                }
                if (currNode.leftNode != null)
                {
                    roomName += 'L';
                }
                if (currNode.rightNode != null)
                {
                    roomName += 'R';
                }
                if (currNode.upNode != null)
                {
                    roomName += 'T';
                }

                GameObject roomToSpawn = pickRoom(roomName);

                //spawn room
                if (roomToSpawn != null) 
                {
                    rooms.Add(Instantiate(roomToSpawn, spawnPos, Quaternion.identity));
                    Debug.Log("Room spawn success : " + roomName);
                }
                else
                {
                    Debug.Log("Room spawn fail : " + roomName);
                }
            }
        }

        //finished room gen
        stopGenerating = true;
    }

    // Update is called once per frame
    void Update()
    {

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
    public GameObject pickRoom(string roomName) //give in BLRT, alphabetical order 
    {
        //template name are in BLRT
        switch (roomName)
        {
            case "B":
                return B;                

            case "BL":
                return BL;

            case "BLR":
                return BLR;                

            case "BLRT":
                return BLRT;
                
            case "BLT":
                return BLT;                

            case "BR":
                return BR;
                
            case "BRT":
                return BRT;                

            case "BT":
                return BT;
                
            case "L":
                return L;                

            case "LR":
                return LR;
                
            case "LRT":
                return LRT;                

            case "LT":
                return LT;
                
            case "R":
                return R;                

            case "RT":
                return RT;            

            case "T":
                return T;

        }

        return null;
    }
}
