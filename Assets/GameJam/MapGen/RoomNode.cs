using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode
{
    public RoomNode upNode, downNode, leftNode, rightNode = null;
    public bool spawned;
    public bool filled;

    //For distance
    public float distFromSource;
    public RoomNode prevNode;

    public RoomNode()
    {
        spawned = false;
        filled = false;

        distFromSource = float.PositiveInfinity;
        prevNode = null;
    }
}
