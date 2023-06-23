using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPSlider : MonoBehaviour //can gradually slide instead of instant slide in future
{
    [SerializeField] PlayerStats playerStats;
    Slider xpSlider;
    [SerializeField] Text lvlNo;

    void Start()
    {
        xpSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        xpSlider.maxValue = playerStats.levelUpCost;
        xpSlider.value = playerStats.currentExperiencePoints;

        lvlNo.text = playerStats.currentLevel.ToString();
    }
}
