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
        Vector3 startPos = startRoomTransform.position - vertDist * (maxWidth - 1 / 2) - horizontalDist * (maxWidth - 1 / 2);

        for (int i = 0; i < roomNodeArray.Length; ++i)
        {
            RoomNode currNode = roomNodeArray[i];

            if (currNode.filled)
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

                //Instantiate(roomToSpawn, spawnPos, Quaternion.identity);
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
        // //template name are in BLRT
        // switch (roomName)
        // {
        //     case "B":
        //         num = Random.Range(0, templates.B.Length);
        //         //room = templates.B[num];
        //         return templates.B[num];
                

        //     case "BL":
        //         num = Random.Range(0, templates.BL.Length);
        //         //room = templates.BL[num];
        //         return templates.BL[num];
                

        //     case "BR":
        //         num = Random.Range(0, templates.BR.Length);
        //         //room = templates.BR[num];
        //         return templates.BR[num];
                

        //     case "TB":
        //         num = Random.Range(0, templates.TB.Length);
        //         //room = templates.TB[num];
        //         return templates.TB[num];
                

        //     case "LRB":
        //         num = Random.Range(0, templates.LRB.Length);
        //         //room = templates.LRB[num];
        //         return templates.LRB[num];
                

        //     case "LTB":
        //         num = Random.Range(0, templates.LTB.Length);
        //         //room = templates.LTB[num];
        //         return templates.LTB[num];
                

        //     case "RTB":
        //         num = Random.Range(0, templates.RTB.Length);
        //         //room = templates.RTB[num];
        //         return templates.RTB[num];
                

        //     case "L":
        //         num = Random.Range(0, templates.L.Length);
        //         //room = templates.L[num];
        //         return templates.L[num];
                

        //     case "LR":
        //         num = Random.Range(0, templates.LR.Length);
        //         //room = templates.LR[num];
        //         return templates.LR[num];
                

        //     case "LRT":
        //         num = Random.Range(0, templates.LRT.Length);
        //         //room = templates.LRT[num];
        //         return templates.LRT[num];
                

        //     case "R":
        //         num = Random.Range(0, templates.R.Length);
        //         //room = templates.R[num];
        //         return templates.R[num];
                

        //     case "T":
        //         num = Random.Range(0, templates.T.Length);
        //         //room = templates.T[num];
        //         return templates.T[num];
                

        //     case "TL":
        //         num = Random.Range(0, templates.TL.Length);
        //         //room = templates.TL[num];
        //         return templates.TL[num];
                

        //     case "TR":
        //         num = Random.Range(0, templates.TR.Length);
        //         //room = templates.TR[num];
        //         return templates.TR[num];
                

        //     default:
        //         break;


        // }

        return null;
    }
}
