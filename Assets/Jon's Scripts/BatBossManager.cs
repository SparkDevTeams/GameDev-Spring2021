using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossManager : MonoBehaviour
{
    private Transform playerTarget;
    private BatBossBreathAttack breathAttack;
    private BatBossFlyAttack flyAttack;
    private BatBossCrystalRoar roar;
    [SerializeField]
    private BatBossDivePoint centerPoint;
    [SerializeField]
    private int totalHp;
    private int hp;
    private bool invincible = false;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = totalHp;
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        breathAttack = GetComponent<BatBossBreathAttack>();
        flyAttack = GetComponent<BatBossFlyAttack>();
        roar = GetComponent<BatBossCrystalRoar>();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Damage(int dmg) 
    {
        if (isActiveAndEnabled && !invincible) 
        {
            hp -= dmg;

            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Activate() 
    {
        active = true;
        flyAttack.ActivatePatterns();
    }
}
