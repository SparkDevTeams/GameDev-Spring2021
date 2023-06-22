using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{

    public int hp, startHp = 3;
    private bool isInvincible = false;
    private float invincibleTimer;
    [SerializeField] private GameObject soul; //change this to weap upgrades
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        hp = startHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleTimer > 0.0f){
            invincibleTimer -= Time.deltaTime;
        }
        else   
            isInvincible = false;
    }

    public void Damage()
    {
        if (!isInvincible)
        {
            hp--;
            if (hp <= 0)
            {
                //Drop stuff
                Instantiate(soul, transform.position, transform.rotation);

                //Destroy itself
                Destroy(gameObject);
                return;
            }

            invincibleTimer = 0.01f;
            isInvincible = true;
        }
    }
}
