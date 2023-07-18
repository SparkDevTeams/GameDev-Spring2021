using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int atkDamage; //melee
    [SerializeField] private int dexDamage; //ranged
    [SerializeField] private int soulCount;
    private float playerSpeed;
    public int currentLevel;
    public int currentExperiencePoints;
    public int levelUpCost;
    // Level Up Stats
    public int bonusATK = 0, bonusDEX = 0, bonusVIT = 0; //ATK for more melee dmg, DEX for more ranged dmg, VIT for more health
    public int unusedStatPoints = 0;

    private HealthManager healthManager;
    private move move;

    // For buffs
    public int atkBuff = 10;
    public float speedBuff = 10f;
    //Upgrades

    public List<(string, string)> upgradeDescriptions; // Item1 is the name of upgrade, Item2 is the effect of upgrade

    // Start is called before the first frame update
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
        move = GetComponent<move>();
        levelUpCost = 10 * currentLevel + 10;
        upgradeDescriptions = new List<(string, string)>();
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

    public int getMeleeDamage()
    {
        return atkDamage;
    }

    public int getRangedDamage()
    {
        return dexDamage;
    }

    public void addMeleeDamage(int damage)
    {
        atkDamage += damage;
    }

    public void addRangedDamage(int damage)
    {
        dexDamage += damage;
    }

    public void GainExperience(int experience)
    {
        currentExperiencePoints += experience;
        if (currentExperiencePoints >= levelUpCost)
        {
            currentLevel++;
            Debug.Log(currentLevel);
            currentExperiencePoints -= levelUpCost;
            healthManager.heal(1);
            unusedStatPoints++;

            levelUpCost = 10 * currentLevel + 10;
        }
    }

    public void IncreaseStats(int statType)
    {
        if (unusedStatPoints <= 0)
            return;

        switch (statType)
        {
            case 0:
                ++bonusATK;
                addMeleeDamage(1);
                Debug.Log("IncreaseStats : bonusATK = " + bonusATK);
                break;
            case 1:
                ++bonusDEX;
                addRangedDamage(1);
                Debug.Log("IncreaseStats : bonusDEX = " + bonusDEX);
                break;
            case 2:
                ++bonusVIT;
                healthManager.addMaxHealth(1);
                Debug.Log("IncreaseStats : bonusDEF = " + bonusVIT);
                break;
            default:
                Debug.Log("IncreaseStats Error : INVALID STAT TYPE");
                break;
        }

        unusedStatPoints--;
    }

    public int GetBonusStats(int statType)
    {
        switch (statType)
        {
            case 0:
                Debug.Log("GetBonusStats : bonusATK = " + bonusATK);
                return bonusATK;
            case 1:
                Debug.Log("GetBonusStats : bonusDEX = " + bonusDEX);
                return bonusDEX;
            case 2:
                Debug.Log("GetBonusStats : bonusDEF = " + bonusVIT);
                return bonusVIT;
            default:
                Debug.Log("GetBonusStats : INVALID STAT TYPE");
                return 0;

        }
    }

    public void AddBuffs(int buffType)
    {
        switch (buffType)
        {
            case 0:
                Debug.Log("Increase attack buff");
                addMeleeDamage(atkBuff);
                addRangedDamage(atkBuff);
                break;
            case 1:
                Debug.Log("Speed up buff");
                move.addSpeed(speedBuff);
                break;
            case 2:
                Debug.Log("Evasion buff");
                healthManager.setInvis(10);
                break;
            case 3:
                Debug.Log("Poisonous buff");
                break;
            case 4:
                Debug.Log("No block buff");
                break;
            default:
                break;
        }
    }

    public void RemoveBuffs(int buffType)
    {
        switch (buffType)
        {
            case 0: 
                Debug.Log("Remove attack buff");
                addMeleeDamage(atkBuff * -1);
                addRangedDamage(atkBuff * -1);
                break;
            case 1:
                Debug.Log("Remove Speed up buff");
                move.addSpeed(speedBuff * -1);
                break;
            case 2:
                Debug.Log("Remove Evasion buff");
                break;
            case 3:
                Debug.Log("Remove Poisonous buff");
                break;
            case 4:
                Debug.Log("Remove No block buff");
                break;
            default:
                break;
        }
    }
}
