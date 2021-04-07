using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int health;
    private int maxHealth;
    private HP_Display health_display;
    void Start()
    {
        health_display = FindObjectOfType<HP_Display>();
        health = health_display.getTargetHealth();
        maxHealth = health_display.getMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(int damage)
    {
        health -= damage;
        health_display.UpdateHealth(health);

    }

    public void heal(int heal)
    {
        health += heal;
        //health_display.UpdateHealth(health);
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }
}
