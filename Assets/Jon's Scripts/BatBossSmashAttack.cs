using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossSmashAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject crystal;
    [SerializeField]
    private Vector2 leftLocalPositionOffset;
    [SerializeField]
    private Vector2 rightLocalPositionOffset;
    [SerializeField]
    private float attackRadius = 1.2f;
    [SerializeField]
    private float timeBetweenSpawns = 0.12f;
    [SerializeField]
    private int totalSpawns = 3;
    [SerializeField]
    private int crystalsPerSpawn = 4;
    private float spawnTimer = 0.0f;
    private int numOfSpawns = 0;
    private bool attacking = false;
    private bool leftAttack = false;
    private bool rightAttack = false;

    void Start()
    {
        attacking = false;
        leftAttack = false;
        rightAttack = false;
        numOfSpawns = 0;
        spawnTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking) 
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0.0f) 
            {
                Spawn();
                numOfSpawns++;

            }
        }
        
    }

    private void Spawn() 
    {
        for (int spawns = 0; spawns < crystalsPerSpawn; spawns++) 
        {
            Vector3 randomPos = Random.insideUnitCircle * attackRadius;

            if (leftAttack) 
            {
                Instantiate(crystal, transform.position + (Vector3)leftLocalPositionOffset 
                    + randomPos, Quaternion.identity);
            }

            if (rightAttack) 
            {
                Instantiate(crystal, transform.position + (Vector3)rightLocalPositionOffset
                    + randomPos, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + (Vector3)leftLocalPositionOffset, attackRadius);
        Gizmos.DrawWireSphere(transform.position + (Vector3)rightLocalPositionOffset, attackRadius);
    }
}
