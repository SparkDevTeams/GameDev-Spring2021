using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int attackDamage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int rollSpeed;
    [SerializeField] private int stunTime;
    [SerializeField] private int soulCount;
    public int currentLevel;
    public int maxLevel;
    public int currentExperiencePoints;
    public int[] levelUpThresholds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getSoulCount()
    {
        return soulCount;
    }

    public void addSouls(int souls)
    {
        soulCount += souls;
    }

    public void spendSouls(int cost)
    {
        soulCount -= cost;
    }

    public int getDamage()
    {
        return attackDamage;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public void addDamage(int damage)
    {
        attackDamage += damage;
    }

    public void GainExperience(int experience)
    {
        currentExperiencePoints += experience;
        if (currentLevel < maxLevel && currentExperiencePoints >= levelUpThresholds[currentLevel - 1])
        {
            currentLevel++;
            GetComponent<HealthManager>().heal(HealthManager.staticMaxHealth);
            // Add more scripts here -> Level up stats permanently, etc
        }
    }

}
