using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int health;
    [SerializeField]private int maxHealth;
    public static int staticMaxHealth;
    private HP_Display health_display;
    private float invincibiltyTime;
    public GameObject loseScreen;
    private GameObject HP_EXP_Header;

    void Start()
    {
        HP_EXP_Header = GameObject.Find("HP_EXP_Header");
        health_display = HP_EXP_Header.GetComponentInChildren<HP_Display>();

        //health_display = FindObjectOfType<HP_Display>();
        health = health_display.getTargetHealth();
        maxHealth = health_display.getMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        staticMaxHealth = maxHealth;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (invincibiltyTime > 0) {
            GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);

            invincibiltyTime -= Time.deltaTime;

            if (invincibiltyTime <= 0) {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    public void damage(int damage)
    {
        if (invincibiltyTime > 0.0f) {
            return;
        }

        health -= damage;
        health_display.UpdateHealth(health);
        invincibiltyTime = 0.5f + (damage * 0.25f);
    }

    public void damage(int dmg, float stunTime) {
        if (GetComponent<move>().Mode == "Dead") {
            return;
        }

        if (invincibiltyTime > 0.0f)
        {
            return;
        }

        damage(dmg);

        string state = "Hurt";
        if (health <= 0) {
            state = "Dead";
            StartCoroutine(GameOver());
        }

        GetComponent<move>().SetAnimation(state, stunTime);
    }

    public void heal(int heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
        
        health_display.UpdateHealth(health);
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void addMaxHealth(int healthIncrease)
    {
        maxHealth += healthIncrease;
        health_display.UpdateMaxHealth(maxHealth);
    }

    public void addHealth(int healthIncrease)
    {
        health += healthIncrease;
        health_display.UpdateHealth(health);
    }

    public void setInvis(float invincibility) {
        invincibiltyTime = invincibility;
    }

    public IEnumerator GameOver() {
        yield return new WaitForSeconds(3);
        loseScreen.SetActive(true);
        //SceneManager.LoadScene("MainMenu1");
    }
}
