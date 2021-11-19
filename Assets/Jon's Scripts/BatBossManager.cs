using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossManager : MonoBehaviour
{
    private Transform playerTarget;
    private BatBossBreathAttack breathAttack;
    private BatBossFlyAttack flyAttack;
    private BatBossCrystalRoar roar;
    private EnemyManager manager;
    [SerializeField]
    private BatBossDivePoint centerPoint;
    private bool invincible = false;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        breathAttack = GetComponent<BatBossBreathAttack>();
        flyAttack = GetComponent<BatBossFlyAttack>();
        roar = GetComponent<BatBossCrystalRoar>();
        manager = GetComponent<EnemyManager>();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Activate() 
    {
        active = true;
        flyAttack.ActivatePatterns();
    }
}
