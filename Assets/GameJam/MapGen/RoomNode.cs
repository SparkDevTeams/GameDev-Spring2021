using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode
{
    public RoomNode upNode, downNode, leftNode, rightNode = null;
    public NewRoomSpawner roomSpawner;
    public bool spawned;
    public bool filled;

    public RoomNode()
    {
        spawned = false;
        filled = false;
        roomSpawner = null;
    }
}
