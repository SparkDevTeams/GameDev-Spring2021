using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM {
    NONE,
    FIRE,
    THUNDER,
    EARTH,
    SHADOW,
    POTION
}

public class Inventory : MonoBehaviour
{
    const float magicTime = 0.25f;
    const float animTime = 0.4f;
    //checks whether an item is in an inventiry slot
    //public bool isFull;
    public int Capacity {
        get { return 4; }
    }

    public List<ITEM> slots = new List<ITEM>();

    public int activeSlot = 0;

    [SerializeField]
    private GameObject fireShot;
    [SerializeField]
    private GameObject sparkField;
    [SerializeField]
    private GameObject sandBall;

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

        if (Input.GetButtonDown("UseItem")  && slots.Count > 0 && (gameObject.GetComponent<Move>().Mode == "Idle" || gameObject.GetComponent<Move>().Mode == "Walk")) {
            Debug.Log("Execute Item Effect");
            StartCoroutine( UseItem(slots[activeSlot]));
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

    IEnumerator UseItem(ITEM type) {
        GameObject obj;
        switch (type) {
            case ITEM.FIRE:

                gameObject.GetComponent<Move>().SetAnimation("Magic", animTime);
                yield return new WaitForSeconds(magicTime);

                //create fire prefab
                obj = Instantiate(fireShot);
                
                switch (gameObject.GetComponent<Move>().Direction) {
                    case "Front":
                        obj.transform.localPosition = new Vector2(transform.localPosition.x - 1 , transform.localPosition.y  - 0.5f);
                        obj.transform.localEulerAngles = new Vector3(0,0,270);
                        break;
                    case "Back":
                        obj.transform.localPosition = new Vector2(transform.localPosition.x + 1, transform.localPosition.y + 1.5f);
                        obj.transform.localEulerAngles = new Vector3(0, 0, 90);
                        break;
                    default:
                        if (transform.localScale.x < 0)
                        {
                            obj.transform.localPosition = new Vector2(transform.localPosition.x + 1, transform.localPosition.y);
                            obj.transform.localEulerAngles = new Vector3(0, 0, 0);
                        }
                        else {
                            obj.transform.localPosition = new Vector2(transform.localPosition.x - 1, transform.localPosition.y + 2);
                            obj.transform.localEulerAngles = new Vector3(0, 0, 180);
                        }
                        break;
                }
                break;
            case ITEM.THUNDER:
                gameObject.GetComponent<Move>().SetAnimation("Magic", animTime);
                yield return new WaitForSeconds(magicTime);
                obj = Instantiate(sparkField);
                obj.transform.SetParent(transform);
                obj.transform.localPosition = new Vector3(0, 1, 0);
                break;
            case ITEM.EARTH:
                gameObject.GetComponent<Move>().SetAnimation("Magic", animTime);
                yield return new WaitForSeconds(magicTime);
                obj = Instantiate(sandBall);
                switch (gameObject.GetComponent<Move>().Direction)
                {
                    case "Front":
                        obj.transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.5f);
                        obj.transform.localEulerAngles = new Vector3(0, 0, 270);
                        break;
                    case "Back":
                        obj.transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 1.5f);
                        obj.transform.localEulerAngles = new Vector3(0, 0, 90);
                        break;
                    default:
                        if (transform.localScale.x < 0)
                        {
                            obj.transform.localPosition = new Vector2(transform.localPosition.x + 1, transform.localPosition.y + 1);
                            obj.transform.localEulerAngles = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            obj.transform.localPosition = new Vector2(transform.localPosition.x - 1, transform.localPosition.y + 1);
                            obj.transform.localEulerAngles = new Vector3(0, 0, 180);
                        }
                        break;
                }
                break;
            case ITEM.SHADOW:
                //Set Invis Frames Here
                GetComponent<HealthManager>().setInvis(5);
                break;
            case ITEM.POTION:
                //Increase HP
                GetComponent<HealthManager>().heal(12);
                break;
        }
    }

}
