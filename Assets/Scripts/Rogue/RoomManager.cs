using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private bool isCleared;
    [SerializeField] private bool playerInside;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getIsCleared()
    {
        return isCleared;
    }

    public void setIsCleared(bool isCleared)
    {
        this.isCleared = isCleared;
    }

    public bool getPlayerInside()
    {
        return playerInside;
    }

    public void setPlayerInside(bool playerInside)
    {
        this.playerInside = playerInside;
    }
}
