using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    public bool isDeadend;

    public bool allowed;

    public string name;

    public bool limited;
    private int limitCount;

    // Start is called before the first frame update
    void Start()
    {
        //For demonstration purposes only
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);

        //if (limited)
        //{
        //    templates.decrementLimit();
        //}



    }

    // Update is called once per frame
    void Update()
    {
        //if(templates.getLimit() <= 0)
        //{
        //    setAllowed(false);
        //}
    }

    public bool getAllowed()
    {
        return allowed;
    }

    public bool getIsDeadend()
    {
        return isDeadend;
    }

    public void setAllowed(bool allow)
    {
        allowed = allow;
    }

    public string getName()
    {
        return name;
    }
}
