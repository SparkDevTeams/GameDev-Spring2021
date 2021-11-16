using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private int itemID;
    [SerializeField] private int cost;
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<TextMesh>().text = cost.ToString();
    }

    private void addStats(Collider2D collision)
    {
        collision.gameObject.GetComponent<HealthManager>().addMaxHealth(maxHealth);
        collision.gameObject.GetComponent<move>().addSpeed(speed);
        collision.gameObject.GetComponent<HealthManager>().addHealth(health);

        collision.gameObject.GetComponent<PlayerStats>().spendSouls(cost);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            if (cost <= collision.gameObject.GetComponent<PlayerStats>().getSoulCount())
            {
                addStats(collision);
            }


        }
    }

}
