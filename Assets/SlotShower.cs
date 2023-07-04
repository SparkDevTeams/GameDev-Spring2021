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
    public Sprite dragonDishImg;

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
        //Rotate
        int rot = 0;
        switch (inventory.activeSlot) {
            case 0:
                rot = 0;
                break;
            case 1:
                rot = 90;
                break;
            case 2:
                rot = 180;
                break;
            case 3:
                rot = 270;
                break;

        }


        gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3( 0, 0, -(rot));
        slot1.rectTransform.eulerAngles = new Vector3(0, 0, 0);
        slot2.rectTransform.eulerAngles = new Vector3(0, 0, 0);
        slot3.rectTransform.eulerAngles = new Vector3(0, 0, 0);
        slot4.rectTransform.eulerAngles = new Vector3(0, 0, 0);


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
            default:
                img.color = Color.clear;
                break;
        }
    }
}
