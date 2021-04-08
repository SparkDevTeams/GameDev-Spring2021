using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float stunTime;
    public bool stunned;

    public int hp, startHp = 3;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        stunned = false;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        hp = startHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(stunTime > 0){
            stunned = true;
            stunTime -= Time.deltaTime;
        }
        else   
            stunned = false;

        if(hp <= 0){
            Destroy(gameObject);
        }
    }

    public void Damage(int p){

        hp -= p;
    }

    public void Damage(int dmg, float stun) {
        if (stun > stunTime) {
            stunTime = stun;
        }
        Damage(dmg);
    }
}
