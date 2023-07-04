using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isTeleported;
    [SerializeField] private Transform bossRoom;
    [SerializeField] private DragonBossManager bm;

    void Start()
    {
        isTeleported = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void teleported(bool isTeleport)
    {
        isTeleported = isTeleport;
        bm.Activate();
    }

    public bool getTeleported()
    {
        return isTeleported;
    }

    public Transform getBossRoom()
    {
        return bossRoom;
    }
}
