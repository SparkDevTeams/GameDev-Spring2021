using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] HealthManager healthManager;

    public Text ATKCounter, DEXCounter, VITCounter, StatPointsCounter, HPCounter, XPCounter, LVCounter;

    [SerializeField]
    Text toggleStatsPageButtonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Stats counters
        ATKCounter.text = "ATK : " + playerStats.bonusATK;
        DEXCounter.text = "DEX : " + playerStats.bonusDEX;
        VITCounter.text = "VIT : " + playerStats.bonusVIT;
        StatPointsCounter.text = "StatPoints : " + playerStats.unusedStatPoints;

        HPCounter.text = "Health : " + healthManager.getHealth() + " / " + healthManager.getMaxHealth();
        XPCounter.text = "XP : " + playerStats.currentExperiencePoints;
        LVCounter.text = "Level : " + playerStats.currentLevel;


    }
}
