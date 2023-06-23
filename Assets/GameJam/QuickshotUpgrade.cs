using UnityEngine;

public class QuickshotUpgrade : WeaponUpgrade
{
    private PlayerTest playerTest; //script that does shooting

    protected override void ExtraStart()
    {
        playerTest = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTest>();
    }

    protected override void AddUpgradeDescription()
    {
        playerStats.upgradeDescriptions.Add(("Quickshot","Gun shoots faster"));
    }

    protected override void AddUpgradeEffects()
    {
        playerTest.arrowCooldownMultiplier *= 0.5f;
    }
}
