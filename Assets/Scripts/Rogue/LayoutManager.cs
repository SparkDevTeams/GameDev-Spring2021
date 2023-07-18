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
        layoutList = generateLayoutList();
        chosenLayout = selectLayout();


        var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
        newLayout.transform.SetParent(this.transform);

        hasSpawned = true;
    }

    public void spawnItemRoom()
    {
        layoutList = generateItemLayoutList();
        chosenLayout = selectLayout();


        var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
        newLayout.transform.SetParent(this.transform);

        hasSpawned = true;
    }

    public void spawnBossRegularRoom()
    {
        layoutList = generateBossLayoutList();
        chosenLayout = selectLayout();


        var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
        newLayout.transform.SetParent(this.transform);

        hasSpawned = true;
    }

    public void spawnShopRegularRoom()
    {
        layoutList = generateShopLayoutList();
        chosenLayout = selectLayout();


        var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
        newLayout.transform.SetParent(this.transform);

        hasSpawned = true;
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
