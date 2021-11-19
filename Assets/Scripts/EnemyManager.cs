using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float stunTime;
    public bool stunned = false;
    private bool isInvincible = false;
    [SerializeField]
    private bool roomIndependent = false;

    public int hp, startHp = 3;

    public Transform target;

    private DoorManager doors = null;
    private RoomTemplates room = null;
    [SerializeField] private GameObject soul;

    // Start is called before the first frame update
    void Start()
    {
        stunned = false;

        target = GameObject.FindGameObjectWithTag("Player").transform;

        if (!roomIndependent)
        {
            doors = GetComponentInParent<DoorManager>();
            room = FindObjectOfType<RoomTemplates>();
        }
        //this.gameObject.SetActive(false);
        //this.enabled = false;

        hp = startHp;
    }

    // Update is called once per frame
    void Update()
    {
        //if (doors.getClosed())
        //{
        //    //this.gameObject.SetActive(true);
            
        //    Debug.Log("Should spawn");
        //}

        if(stunTime > 0.0f){
            stunned = true;
            stunTime -= Time.deltaTime;
        }
        else   
            stunned = false;
    }

    public void Damage(int p)
    {
        if (!isInvincible)
        {
            hp -= p;

            if (hp <= 0)
            {
                if (!roomIndependent)
                {
                    room.getActiveRoom().GetComponent<DoorManager>().killEnemy();
                }

                Instantiate(soul, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    public void Damage(int dmg, float stun) 
    {
        if (!isInvincible)
        {
            if (stun > stunTime)
            {
                stunTime = stun;
            }

            Damage(dmg);
        }
    }

    public bool IsInvincible() 
    {
        return isInvincible;
    }

    public void TriggerInvincibility()
    {
        isInvincible = true;
    }

    public void StopInvincibility() 
    {
        isInvincible = false;
    }
}
