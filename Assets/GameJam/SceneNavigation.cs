using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    private Inventory inventory;
    public ITEM itemType;
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
        if (inventory.slots.Count < inventory.Capacity)
        {
            inventory.slots.Add(itemType);
        }
        else
        {
            inventory.slots[inventory.activeSlot] = itemType;
        }
    }
}
