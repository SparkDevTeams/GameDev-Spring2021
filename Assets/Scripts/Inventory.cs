using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM {
    FIRE,
    THUNDER,
    EARTH,
    SHADOW,
    POTION
}

public class Inventory : MonoBehaviour
{
    //checks whether an item is in an inventiry slot
    //public bool isFull;
    public int Capacity {
        get { return 4; }
    }

    public List<ITEM> slots = new List<ITEM>();

    public int activeSlot = 0;

    void Update()
    {
        if (slots.Count < 1) {
            return;
        }

        if (Input.GetButtonDown("ShiftRight"))
        {
            Debug.Log("Shift Items Right");
            activeSlot = (activeSlot + 1) % slots.Count;
        }
        else if (Input.GetButtonDown("ShiftLeft")) {
            Debug.Log("Shift Items Left");
            if (activeSlot > 0)
            {
                activeSlot = (activeSlot - 1) % slots.Count;
            }
            else {
                activeSlot = slots.Count - 1;
            }
        }

        if (Input.GetButtonDown("UseItem")  && slots.Count > 0) {
            Debug.Log("Execute Item Effect");
            
            slots.RemoveAt(activeSlot);

            if (activeSlot > 0)
            {
                activeSlot = (activeSlot - 1) % slots.Count;
            }
            else
            {
                activeSlot = slots.Count - 1;
            }

            if (activeSlot < 0) { activeSlot = 0; }
        }
    }

}
