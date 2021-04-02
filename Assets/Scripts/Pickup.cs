using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    //public GameObject itemButton;
    public ITEM itemType;

    public void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Play Sound Effect

        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Inventory>() != null) {
                inventory = other.gameObject.GetComponent<Inventory>();
                if (inventory.slots.Count < inventory.Capacity)
                {
                    //Add to last slot available
                    Debug.Log("Item Added");
                    inventory.slots.Add(itemType);
                }
                else {
                    //Pop out the active slot
                    Debug.Log("Item Replaced");
                    inventory.slots[inventory.activeSlot] = itemType;
                    
                }

                Destroy(gameObject);
            }

            /*for (int i = 0; i < inventory.slots.Count; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //Item can be added because it is empty
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                    //test
                }
            }*/
        }
    }
}
