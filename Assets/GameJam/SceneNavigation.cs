using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    private Inventory inventory;
    public ITEM itemType;
    public CookingManager cookingManager;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        move.gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
        move.gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void DishToHotkey()
    {
        switch (itemType)
        {
            case ITEM.bakKutTeh:
                if (cookingManager.bakKutTehCount > 0)
                {
                    if (inventory.slots.Count < inventory.Capacity)
                    {
                        //Add to last slot available
                        cookingManager.bakKutTehCount -= 1;
                        cookingManager.bakKutTehText.text = "Bak Kut Teh: " + cookingManager.bakKutTehCount;
                        Debug.Log("Item Added");
                        inventory.slots.Add(itemType);
                    }
                    else
                    {
                        //Pop out the active slot
                        cookingManager.bakKutTehCount -= 1;
                        cookingManager.bakKutTehText.text = "Bak Kut Teh: " + cookingManager.bakKutTehCount;
                        Debug.Log("Item Replaced");
                        inventory.slots[inventory.activeSlot] = itemType;
                    }
                }
            break;

        }
    }
} 
