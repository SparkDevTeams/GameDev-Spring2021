using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    GameObject[] shopItemPool;
    GameObject chosenItem;

    private ShopManager shop;
    // Start is called before the first frame update
    void Start()
    {
        //shopItemPool = generateItemList();
        shop = FindObjectOfType<ShopManager>();
        chosenItem = shop.getShopPoolItem();
        shop.removeFromPool(chosenItem);

        Instantiate(chosenItem, this.transform.position, this.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject[] generateItemList()
    {
        return Resources.LoadAll<GameObject>("Shop Items");
    }

    GameObject selectItem()
    {
        int num = Random.Range(0, shopItemPool.Length);
        return shopItemPool[num];
    }
}
