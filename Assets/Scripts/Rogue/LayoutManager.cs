using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    private GameObject parent;
    private GameObject chosenLayout;

    private RoomTemplates templates;

    public GameObject [] layoutList;

    private DoorManager door;

    [SerializeField]private bool specialSpawned;
    private int spawnCode;
    private bool hasSpawned;
    private bool setSpawn;
    // Start is called before the first frame update
    void Start()
    {
        specialSpawned = false;
        hasSpawned = false;
        setSpawn = false;
        spawnCode = 0;
        templates = FindObjectOfType<RoomTemplates>();

        if (GetComponentInParent<AddRoom>().getIsDeadend())
        {
            if (!templates.getItemSpawned())
            {
                layoutList = generateItemLayoutList();
                templates.setItemSpawned(true);
                specialSpawned = true;
                Debug.Log("Item room should spawn");
            }
            else if (!templates.getBossSpawned())
            {
                layoutList = generateBossLayoutList();
                templates.setBossSpawned(true);
                specialSpawned = true;
                Debug.Log("Boss room should spawn");
            }
            else if (!templates.getShopSpawned())
            {
                layoutList = generateShopLayoutList();
                templates.setShopSpawned(true);
                specialSpawned = true;
                Debug.Log("Shop room should spawn");
            }
            else
            {
                layoutList = generateLayoutList();
            }
        }
        else
        {
            layoutList = generateLayoutList();

        }
        //parent = this.transform.parent.gameObject;
        //layoutList = generateLayoutList();
        chosenLayout = selectLayout();


        var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
        newLayout.transform.SetParent(this.transform);

    }

    //Update is called once per frame
    void Update()
    {
        //if (!hasSpawned && setSpawn)
        //{
        //    switch (spawnCode)
        //    {
        //        case 1:
        //            spawnRegularRoom();
        //            break;

        //        case 2:
        //            spawnItemRoom();
        //            break;

        //        case 3:
        //            spawnShopRegularRoom();
        //            break;

        //        case 4:
        //            spawnBossRegularRoom();
        //            break;
        //    }
        //    hasSpawned = true;
        //}

    }

    public void setSpawnCode(int spawncode)
    {
        spawnCode = spawncode;
    }

    public void spawnLayouts()
    {
        setSpawn = true;
    }

    GameObject selectLayout()
    {
        int num = Random.Range(0, layoutList.Length);
        return layoutList[num];
    }

    void spawnRegularRoom()
    {
        layoutList = generateLayoutList();
        chosenLayout = selectLayout();


        var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
        newLayout.transform.SetParent(this.transform);
    }

    void spawnItemRoom()
    {
        layoutList = generateItemLayoutList();
        chosenLayout = selectLayout();


        var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
        newLayout.transform.SetParent(this.transform);
    }

    void spawnBossRegularRoom()
    {
        layoutList = generateBossLayoutList();
        chosenLayout = selectLayout();


        var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
        newLayout.transform.SetParent(this.transform);
    }

    void spawnShopRegularRoom()
    {
        layoutList = generateShopLayoutList();
        chosenLayout = selectLayout();


        var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
        newLayout.transform.SetParent(this.transform);
    }

    GameObject [] generateLayoutList()
    {
        return Resources.LoadAll<GameObject>("Regular Rooms");
    }

    GameObject [] generateItemLayoutList()
    {
        return Resources.LoadAll<GameObject>("Item Rooms");
    }

    GameObject[] generateBossLayoutList()
    {
        return Resources.LoadAll<GameObject>("Boss Rooms");
    }

    GameObject[] generateShopLayoutList()
    {
        return Resources.LoadAll<GameObject>("Shop Rooms");
    }

    
}
