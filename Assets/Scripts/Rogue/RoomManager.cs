using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private bool isCleared;
    [SerializeField] private bool playerInside;

    //two snapshots of the configurations of a audio mixer compenent, will switch between as there are enemies in the room
   
    private RoomTemplates all;

    
    // Start is called before the first frame update
    void Start()
    {
      //  mixer = GameObject.GetComponent<AudioMixer>();
      //  calmState = FindSnapshot("Normal State");
        // enemyState = FindSnapshot("Enemy State");
        playerInside = false;
        all = FindObjectOfType<RoomTemplates>();
    }

    // If the player enters a room, that room is set as the active room
    //This is used for camera control and spawning enemies
    void Update()
    {
     
        if (playerInside)
        {
            all.setActiveRoom(this.gameObject);
        }
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
