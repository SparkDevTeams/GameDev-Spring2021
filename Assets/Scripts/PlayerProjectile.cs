using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour //Player ranged projectile
{

    public float speed;
    public Rigidbody2D rb;

    public Vector2 v;

    private PlayerManager t;

    public int angle;

    public int power;

    public float cosineTest, sineTest;
    private PlayerStats stats;
    bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();

        power = stats.getRangedDamage();

        rb = GetComponent<Rigidbody2D>();
        

        t = FindObjectOfType<PlayerManager>();

        angle = t.angle;
        

        v = new Vector2(Mathf.Cos(angle * Mathf.PI/180),Mathf.Sin(angle* Mathf.PI/180));

        rb.velocity = speed * v; 
        
    }


    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if(hitInfo.tag == "Enemy" || hitInfo.name == "Walls" || hitInfo.tag == "Door" || hitInfo.name == "Layout Walls" || hitInfo.name == "Chest") // test wall collider 
        {
            if (!hit)
            {
                hit = true;

                Destroy(gameObject);

                if(hitInfo.tag == "Enemy")
                {
                    hitInfo.GetComponent<EnemyManager>().Damage(power);
                }

                if (hitInfo.tag == "Chest") {
                    hitInfo.GetComponent<ChestManager>().Damage();
                }
            }
        }
    }
    
}
