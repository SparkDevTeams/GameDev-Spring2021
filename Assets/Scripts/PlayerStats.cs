using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int atkDamage; //melee
    [SerializeField] private int dexDamage; //ranged
    [SerializeField] private float moveSpeed;
    [SerializeField] private int rollSpeed;
    [SerializeField] private int stunTime;
    [SerializeField] private int soulCount;
    public int currentLevel;
    public int currentExperiencePoints;
    // Level Up Stats
    public int bonusATK = 0, bonusDEX = 0, bonusDEF = 0; //ATK for more melee dmg, DEX for more ranged dmg, DEF for reduced dmg taken

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

    public int getMeleeDamage()
    {
        return atkDamage;
    }

    public int getRangedDamage()
    {
        return dexDamage;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public void addMeleeDamage(int damage)
    {
        atkDamage += damage;
    }

    public void addRangedDamage(int damage)
    {
        atkDamage += damage;
    }

    public void GainExperience(int experience)
    {
        currentExperiencePoints += experience;
        if (currentExperiencePoints >= 2 * currentLevel + 2) //Exp needed to lvl up increases by 25 each level, starts at 50
        {
            currentLevel++; //Increases level
            GetComponent<HealthManager>().heal(1); //Heal by 1
            //Level Up Stats
            IncreaseStats(0);
            IncreaseStats(1);
        }
    }

    public void IncreaseStats(int statType)
    {
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
                ++bonusDEF;
                
                Debug.Log("IncreaseStats : bonusDEF = " + bonusDEF);
                break;
            default:
                Debug.Log("IncreaseStats Error : INVALID STAT TYPE");
                break;

        }
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
                Debug.Log("GetBonusStats : bonusDEF = " + bonusDEF);
                return bonusDEF;
            default:
                Debug.Log("GetBonusStats : INVALID STAT TYPE");
                return 0;

        }
    }
}
