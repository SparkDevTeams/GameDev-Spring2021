using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int health;
    private int maxHealth;
    private HP_Display health_display;
    private float invincibiltyTime;

    void Start()
    {
        health_display = FindObjectOfType<HP_Display>();
        health = health_display.getTargetHealth();
        maxHealth = health_display.getMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (GetComponent<Move>().Mode == "Dead") {
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

        GetComponent<Move>().SetAnimation(state, stunTime);
    }

    public void heal(int heal)
    {
        health += heal;
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

    public void setInvis(float invincibility) {
        invincibiltyTime = invincibility;
    }

    public IEnumerator GameOver() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu1");
    }
}
