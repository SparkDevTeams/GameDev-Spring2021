using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int attackDamage;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int rollSpeed;
    [SerializeField] private int stunTime;
    [SerializeField] private int soulCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getSoulCount()
    {
        return soulCount;
    }

    public void spendSouls(int cost)
    {
        soulCount -= cost;
    }
}
