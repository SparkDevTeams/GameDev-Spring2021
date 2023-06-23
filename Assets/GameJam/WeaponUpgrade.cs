using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    protected bool picked = false;
    protected PlayerStats playerStats; //To store upgrades description so player know what upgrade they have (actual upgrade effect will be )

    public void Start() //Dont override this
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        ExtraStart();
    }

    protected virtual void ExtraStart() //Override this
    {

    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!picked) {
                picked = true;
                
                AddUpgradeDescription();
                AddUpgradeEffects();

                Destroy(gameObject);
            }
        }
    }

    protected virtual void AddUpgradeDescription() //Override this
    {
        playerStats.upgradeDescriptions.Add(("BaseUpgrade","Does nothing"));
    }

    protected virtual void AddUpgradeEffects() //Override this
    {

    }
}
