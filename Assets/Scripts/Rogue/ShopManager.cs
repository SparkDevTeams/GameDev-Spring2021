using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    List<GameObject> shopPool;
    // Start is called before the first frame update
    void Start()
    {
        shopPool = new List<GameObject>();
        generateItemList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateItemList()
    {
        GameObject[] temp = Resources.LoadAll<GameObject>("Shop Items");
        
        
        for(int i = 0; i < temp.Length; i++)
        {
            shopPool.Add(temp[i]);
        }
        
    }

    public GameObject getShopPoolItem()
    {
        Debug.Log("Bruuhhhh " + shopPool.Count);
        int num = Random.Range(0, shopPool.Count);
        return shopPool[num];
    }

    public void removeFromPool(GameObject item)
    {
        shopPool.Remove(item);
    }
}
