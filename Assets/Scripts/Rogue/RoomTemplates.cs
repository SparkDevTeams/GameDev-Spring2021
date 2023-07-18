using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    int maxWidth; //max size is square
    List<RoomNode> roomNodeList = new List<RoomNode>();
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

        for (int i = 0; i < maxWidth * maxWidth; ++i)
        {
            roomNodeList.Add(new RoomNode());
            roomNodeList[i].index = i;
        }

        int middleIndex = (maxWidth * maxWidth - 1) / 2;

        RoomNode startMiddleNode = roomNodeList[middleIndex];
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
                        nextNode = roomNodeList[nextIndex];

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
                        nextNode = roomNodeList[nextIndex];

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
                        nextNode = roomNodeList[nextIndex];
                            
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
                        nextNode = roomNodeList[nextIndex];
                            
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
        for (int i = 0; i < roomNodeList.Count; ++i)
        {
            RoomNode currNode = roomNodeList[i];

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

        for (int i = 0; i < roomNodeList.Count; ++i)
        {
            RoomNode currNode = roomNodeList[i];

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
                    GameObject roomSpawned = Instantiate(roomToSpawn, spawnPos, Quaternion.identity);
                    currNode.layoutManager = roomSpawned.GetComponent<LayoutManager>();
                    Debug.Log("Room spawn success : " + roomName);
                }
                else
                {
                    Debug.Log("Room spawn fail : " + roomName);
                }
            }
        }

        //find room dists
        int bossRoomIndex = findDistancesFromSource(middleIndex);
        int chestRoomIndex = findDistancesFromSource(bossRoomIndex);
        Debug.Log("BOSS : " + bossRoomIndex + " CHEST: " + chestRoomIndex);
        //call layout manager functions
        for (int i = 0; i < roomNodeList.Count; ++i)
        {
            RoomNode currNode = roomNodeList[i];

            if (currNode.filled && currNode.index != middleIndex)
            {
                if (!bossRoomCreated && currNode.index == bossRoomIndex)
                {
                    //boss room (furthest)
                    Debug.Log("Furthest room index is : " + currNode.index);

                    bossRoomCreated = true;
                    currNode.layoutManager.spawnBossRegularRoom();
                }
                else if (!itemRoomCreated && currNode.index == chestRoomIndex)
                {
                    //chest room (2nd furthest)
                    Debug.Log("2nd Furthest room index is : " + currNode.index);

                    itemRoomCreated = true;
                    currNode.layoutManager.spawnItemRoom();
                }
                else
                {
                    //normal room
                    Debug.Log("Room index is : " + currNode.index);

                    currNode.layoutManager.spawnRegularRoom();
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

    public int findDistancesFromSource(int source)
    {
        List<RoomNode> tempNodeList = new List<RoomNode>();

        foreach (RoomNode node in roomNodeList)
        {
            node.distFromSource = float.PositiveInfinity;
            node.prevNode = null;

            tempNodeList.Add(node);
        }
        
        //sort list by index (ascending)
        tempNodeList = tempNodeList.OrderBy(x => x.index).ToList();

        tempNodeList[source].distFromSource = 0;

        RoomNode currNode;

        while (tempNodeList.Count > 0)
        {
            //Order list by distFromSource (ascending)
            tempNodeList = tempNodeList.OrderBy(x => x.distFromSource).ToList();

            //Set currNode to node with least dist
            currNode = tempNodeList[0];

            //Remove currNode from list
            tempNodeList.RemoveAt(0);

            //Find neighbours of currNode
            const float neighbourDist = 1; //dist between neighbour and currNode is always 1
            float newDist = float.PositiveInfinity;
            RoomNode neighbourNode = null;

            for (int i = 0; i < 4; ++i)
            {
                switch (i)
                {
                    case 0:
                        neighbourNode = currNode.upNode;
                        break;
                    case 1:
                        neighbourNode = currNode.downNode;
                        break;
                    case 2:
                        neighbourNode = currNode.leftNode;
                        break;
                    case 3:
                        neighbourNode = currNode.rightNode;
                        break;
                }

                if (neighbourNode != null && tempNodeList.Contains(neighbourNode))
                {                    
                    newDist = currNode.distFromSource + neighbourDist;
                    if (newDist < neighbourNode.distFromSource)
                    {
                        neighbourNode.distFromSource = newDist;
                        neighbourNode.prevNode = currNode;
                    }
                }
            }
        }
        
        //sort list by distFromSource (descending)
        roomNodeList = roomNodeList.OrderByDescending(x => x.distFromSource).ToList();
        for (int i = 0; i < roomNodeList.Count; ++i)
        {
            RoomNode node = roomNodeList[i];
            if (!float.IsInfinity(node.distFromSource))
            {
                return node.index;
            }
        }

        Debug.Log("Room gen bug: No index found for furthest room");
        return -1;        
    }
}
