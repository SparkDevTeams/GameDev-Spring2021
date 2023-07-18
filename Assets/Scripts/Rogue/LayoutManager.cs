using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    private GameObject chosenLayout;

    public GameObject [] layoutList;

    private DoorManager door;
    private bool hasSpawned;

    // Start is called before the first frame update
    void Start()
    {
        hasSpawned = false;

    }

    //Update is called once per frame
    void Update()
    {

    }

    GameObject selectLayout()
    {
        int num = Random.Range(0, layoutList.Length);
        return layoutList[num];
    }

    public void spawnRegularRoom()
    {
        if (!hasSpawned)
        {
            layoutList = generateLayoutList();
            chosenLayout = selectLayout();

            var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
            newLayout.transform.SetParent(this.transform);

            hasSpawned = true;
        }
        else
        {
            Debug.Log("Can't spawn more than 1 room layout in a single room!");
        }
    }

    public void spawnItemRoom()
    {
        if (!hasSpawned)
        {
            layoutList = generateItemLayoutList();
            chosenLayout = selectLayout();

            var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
            newLayout.transform.SetParent(this.transform);

            hasSpawned = true;
        }
        else
        {
            Debug.Log("Can't spawn more than 1 room layout in a single room!");
        }
    }

    public void spawnBossRegularRoom()
    {
        if (!hasSpawned)
        {            
            layoutList = generateBossLayoutList();
            chosenLayout = selectLayout();

            var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
            newLayout.transform.SetParent(this.transform);

            hasSpawned = true;
        }
        else
        {
            Debug.Log("Can't spawn more than 1 room layout in a single room!");
        }
    }

    public void spawnShopRegularRoom()
    {
        if (!hasSpawned)
        {            
            layoutList = generateShopLayoutList();
            chosenLayout = selectLayout();

            var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
            newLayout.transform.SetParent(this.transform);

            hasSpawned = true;
        }
        else
        {
            Debug.Log("Can't spawn more than 1 room layout in a single room!");
        }
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
