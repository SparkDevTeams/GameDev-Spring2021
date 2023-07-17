using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM {
    NONE,
    FIRE,
    THUNDER,
    EARTH,
    SHADOW,
    POTION,
    bakKutTeh,
    feetWingBucket,
    oyakodon,
    rabbitOmelet,
    shellBroccoli
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
    public int slotUsed;

    [SerializeField]
    private GameObject fireShot;
    [SerializeField]
    private GameObject sparkField;
    [SerializeField]
    private GameObject sandBall;
    [SerializeField]
    private GameObject meleeAtkBuff;
    [SerializeField]
    private GameObject rangedAtkBuff;
    [SerializeField]
    private GameObject healBuff;
    [SerializeField]
    private GameObject speedBuff;
    [SerializeField]
    private float buffDuration = 10f;

    public Buffs playerBuff;
    private List<GameObject> buffList;
    move playerMove;


    void Start()
    {
        playerMove = GetComponent<move>();
        playerBuff = GetComponent<Buffs>();
        buffList = new List<GameObject> { meleeAtkBuff, rangedAtkBuff, healBuff, speedBuff };
        playerBuff.InitializeBuffIcons(buffList);
    }

    void Update()
    {
        if (slots.Count < 1)
        {
            return;
        }

        if (playerMove.gameIsPaused == false && slots.Count > 0 && (gameObject.GetComponent<move>().Mode == "Idle" || gameObject.GetComponent<move>().Mode == "Walk"))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("Execute Item Effect");

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    slotUsed = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    slotUsed = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    slotUsed = 2;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    slotUsed = 3;
                }

                StartCoroutine(UseItem(slots[slotUsed]));
                slots.RemoveAt(slotUsed);
            }
        }
    }

    IEnumerator UseItem(ITEM type)
    {
        GameObject obj;
        switch (type)
        {
            case ITEM.FIRE:

                gameObject.GetComponent<move>().SetAnimation("Magic", animTime);
                yield return new WaitForSeconds(magicTime);

                //create fire prefab
                obj = Instantiate(fireShot);

                switch (gameObject.GetComponent<move>().Direction)
                {
                    case "Front":
                        obj.transform.localPosition = new Vector2(transform.localPosition.x - 1, transform.localPosition.y - 0.5f);
                        obj.transform.localEulerAngles = new Vector3(0, 0, 270);
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
                        else
                        {
                            obj.transform.localPosition = new Vector2(transform.localPosition.x - 1, transform.localPosition.y + 2);
                            obj.transform.localEulerAngles = new Vector3(0, 0, 180);
                        }
                        break;
                }
                break;
            case ITEM.THUNDER:
                gameObject.GetComponent<move>().SetAnimation("Magic", animTime);
                yield return new WaitForSeconds(magicTime);
                obj = Instantiate(sparkField);
                obj.transform.SetParent(transform);
                obj.transform.localPosition = new Vector3(0, 1, 0);
                break;
            case ITEM.EARTH:
                gameObject.GetComponent<move>().SetAnimation("Magic", animTime);
                yield return new WaitForSeconds(magicTime);
                obj = Instantiate(sandBall);
                switch (gameObject.GetComponent<move>().Direction)
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
            case ITEM.bakKutTeh:
                //Put Food Buff Here
                GetComponent<PlayerStats>().AddBuffs(0);
                //Increase attack
                int meleeAtk = GetComponent<PlayerStats>().getMeleeDamage();
                int rangedAtk = GetComponent<PlayerStats>().getMeleeDamage();
                Debug.Log("add buff, melee atk" + meleeAtk);
                Debug.Log("add buff, ranged atk" + rangedAtk);

                // Add the buff icon to the container list
                playerBuff.AddBuffIcon(meleeAtkBuff, buffDuration);
                // Add the buff icon to the container list
                playerBuff.AddBuffIcon(rangedAtkBuff, buffDuration);

                yield return new WaitForSeconds(buffDuration);

                GetComponent<PlayerStats>().RemoveBuffs(0);
                //playerBuff.RemoveBuffIcon(meleeAtkBuff);
                //playerBuff.RemoveBuffIcon(rangedAtkBuff);

                meleeAtk = GetComponent<PlayerStats>().getMeleeDamage();
                rangedAtk = GetComponent<PlayerStats>().getMeleeDamage();
                Debug.Log("Remove buff");
                Debug.Log("after remove, melee atk" + meleeAtk);
                Debug.Log("after remove, ranged atk" + rangedAtk);

                break;
            case ITEM.feetWingBucket:
                Debug.Log("Used  fwb");
                break;
            case ITEM.oyakodon:
                Debug.Log("Used  oyakodon");
                break;
            case ITEM.rabbitOmelet:
                Debug.Log("Used  ro");
                break;
            case ITEM.shellBroccoli:
                Debug.Log("Used  sb");
                break;
        }
    }
    
}
