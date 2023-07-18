using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode
{
    public RoomNode upNode, downNode, leftNode, rightNode = null;
    public bool spawned; //for when spawning everything in room is done
    public bool filled; //for when room is in grid
    public int index; //eachroom should have different index
    public LayoutManager layoutManager;

    //For distance
    public float distFromSource;
    public RoomNode prevNode;

    public RoomNode()
    {
        index = -1;

        spawned = false;
        filled = false;

        distFromSource = float.PositiveInfinity;
        prevNode = null;
        layoutManager = null;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        RoomNode other = (RoomNode)obj;
        return index == other.index;
    }

    public override int GetHashCode()
    {
        return index.GetHashCode();
    }
}
