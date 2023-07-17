using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SlotShower : MonoBehaviour
{
    public Sprite fireImg;
    public Sprite thunderImg;
    public Sprite earthImg;
    public Sprite shadowImg;
    public Sprite potionImg;
    public Sprite bakKutTehImg;
    public Sprite feetWingBucketImg;
    public Sprite oyakodonImg;
    public Sprite rabbitOmeletImg;
    public Sprite shellBroccoliImg;

    private Inventory inventory;

    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;

    public Image item1;
    public Image item2;
    public Image item3;
    public Image item4;

    private void Start() {
         GameObject[] obj = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in obj) {
            if (go.GetComponent<Inventory>() != null) {
                inventory = go.GetComponent<Inventory>();
                Debug.Log("Slots got the inventory!");
                break;
            }
        }

        if (inventory == null) {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Image Set
        if (inventory.slots.Count >= 1) {
            setItemImage(item1, inventory.slots[0]);
        } else {
            setItemImage(item1, ITEM.NONE);
        }

        if (inventory.slots.Count >= 2)
        {
            setItemImage(item2, inventory.slots[1]);
        }
        else
        {
            setItemImage(item2, ITEM.NONE);
        }

        if (inventory.slots.Count >= 3)
        {
            setItemImage(item3, inventory.slots[2]);
        }
        else
        {
            setItemImage(item3, ITEM.NONE);
        }

        if (inventory.slots.Count >= 4)
        {
            setItemImage(item4, inventory.slots[3]);
        }
        else
        {
            setItemImage(item4, ITEM.NONE);
        }
        //Image Set

    }

    private void setItemImage(Image img, ITEM type) {
        switch (type) {
            case ITEM.FIRE:
                img.sprite = fireImg;
                img.color = Color.white;
                break;
            case ITEM.THUNDER:
                img.sprite = thunderImg;
                img.color = Color.white;
                break;
            case ITEM.EARTH:
                img.sprite = earthImg;
                img.color = Color.white;
                break;
            case ITEM.SHADOW:
                img.sprite = shadowImg;
                img.color = Color.white;
                break;
            case ITEM.POTION:
                img.sprite = potionImg;
                img.color = Color.white;
                break;
            case ITEM.bakKutTeh:
                img.sprite = bakKutTehImg;
                img.color = Color.white;
                break;
            case ITEM.feetWingBucket:
                img.sprite = feetWingBucketImg;
                img.color = Color.white;
                break;
            case ITEM.oyakodon:
                img.sprite = oyakodonImg;
                img.color = Color.white;
                break;
            case ITEM.rabbitOmelet:
                img.sprite = rabbitOmeletImg;
                img.color = Color.white;
                break;
            case ITEM.shellBroccoli:
                img.sprite = shellBroccoliImg;
                img.color = Color.white;
                break;
            default:
                img.color = Color.clear;
                break;
        }
    }
}
