using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishButton : MonoBehaviour
{
    public ITEM itemType;
    private Inventory inventory;
    public CookingManager cookingManager;
    
    // Start is called before the first frame update
    void Start()
    {
        cookingManager = FindObjectOfType<CookingManager>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void DishToHotkey()
    {
        switch (itemType)
        {
            case ITEM.bakKutTeh:
                if (cookingManager.bakKutTehCount > 0)
                {
                    if (inventory.slots.Count < inventory.Capacity)
                    {
                        //Add to last slot available
                        cookingManager.bakKutTehCount -= 1;
                        cookingManager.bakKutTehText.text = "Bak Kut Teh: " + cookingManager.bakKutTehCount;
                        Debug.Log("Item Added");
                        inventory.slots.Add(itemType);
                    }
                    else
                    {
                        //Pop out the active slot
                        cookingManager.bakKutTehCount -= 1;
                        cookingManager.bakKutTehText.text = "Bak Kut Teh: " + cookingManager.bakKutTehCount;
                        Debug.Log("Item Replaced");
                        inventory.slots[inventory.slotUsed] = itemType;
                    }
                }
            break;
            case ITEM.feetWingBucket:
                if (cookingManager.feetWingBucketCount > 0)
                {
                    if (inventory.slots.Count < inventory.Capacity)
                    {
                        //Add to last slot available
                        cookingManager.feetWingBucketCount -= 1;
                        cookingManager.feetWingBucketText.text = "Feet Wing Bucket: " + cookingManager.feetWingBucketCount;
                        Debug.Log("Item Added");
                        inventory.slots.Add(itemType);
                    }
                    else
                    {
                        //Pop out the active slot
                        cookingManager.feetWingBucketCount -= 1;
                        cookingManager.feetWingBucketText.text = "Feet Wing Bucket: " + cookingManager.feetWingBucketCount;
                        Debug.Log("Item Replaced");
                        inventory.slots[inventory.slotUsed] = itemType;
                    }
                }
            break;
            case ITEM.oyakodon:
                if (cookingManager.oyakodonCount > 0)
                {
                    if (inventory.slots.Count < inventory.Capacity)
                    {
                        //Add to last slot available
                        cookingManager.oyakodonCount -= 1;
                        cookingManager.oyakodonText.text = "Oyakodon: " + cookingManager.oyakodonCount;
                        Debug.Log("Item Added");
                        inventory.slots.Add(itemType);
                    }
                    else
                    {
                        //Pop out the active slot
                        cookingManager.oyakodonCount -= 1;
                        cookingManager.oyakodonText.text = "Oyakodon: " + cookingManager.oyakodonCount;
                        Debug.Log("Item Replaced");
                        inventory.slots[inventory.slotUsed] = itemType;
                    }
                }
            break;
            case ITEM.rabbitOmelet:
                if (cookingManager.rabbitOmeletCount > 0)
                {
                    if (inventory.slots.Count < inventory.Capacity)
                    {
                        //Add to last slot available
                        cookingManager.rabbitOmeletCount -= 1;
                        cookingManager.rabbitOmeletText.text = "Rabbit Omelet: " + cookingManager.rabbitOmeletCount;
                        Debug.Log("Item Added");
                        inventory.slots.Add(itemType);
                    }
                    else
                    {
                        //Pop out the active slot
                        cookingManager.rabbitOmeletCount -= 1;
                        cookingManager.rabbitOmeletText.text = "RabbitOmelet: " + cookingManager.rabbitOmeletCount;
                        Debug.Log("Item Replaced");
                        inventory.slots[inventory.slotUsed] = itemType;
                    }
                }
            break;
            case ITEM.shellBroccoli:
                if (cookingManager.shellBroccoliCount > 0)
                {
                    if (inventory.slots.Count < inventory.Capacity)
                    {
                        //Add to last slot available
                        cookingManager.shellBroccoliCount -= 1;
                        cookingManager.shellBroccoliText.text = "Shell Broccoli: " + cookingManager.shellBroccoliCount;
                        Debug.Log("Item Added");
                        inventory.slots.Add(itemType);
                    }
                    else
                    {
                        //Pop out the active slot
                        cookingManager.shellBroccoliCount -= 1;
                        cookingManager.shellBroccoliText.text = "Shell Broccoli: " + cookingManager.shellBroccoliCount;
                        Debug.Log("Item Replaced");
                        inventory.slots[inventory.slotUsed] = itemType;
                    }
                }
            break;
        }
    }
}
