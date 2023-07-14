using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode
{
    public RoomNode upNode, downNode, leftNode, rightNode;
    public NewRoomSpawner roomSpawner;
    public bool spawned;

    public RoomNode()
    {
        spawned = false;
        roomSpawner = null;
    }
}
