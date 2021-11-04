using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    private GameObject parent;
    private GameObject chosenLayout;

    public GameObject [] layoutList;

    private DoorManager door;
    // Start is called before the first frame update
    void Start()
    {
        //parent = this.transform.parent.gameObject;
        layoutList = generateLayoutList();
        chosenLayout = selectLayout();

        
        var newLayout = Instantiate(chosenLayout, this.transform.position, this.transform.rotation);
        newLayout.transform.SetParent(this.transform);

    }

    GameObject selectLayout()
    {
        int num = Random.Range(0, layoutList.Length);
        return layoutList[num];
    }

    GameObject [] generateLayoutList()
    {
        return Resources.LoadAll<GameObject>("Regular Rooms");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
