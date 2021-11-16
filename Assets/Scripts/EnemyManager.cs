using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float stunTime;
    public bool stunned = false;
    private bool isInvincible = false;

    public int hp, startHp = 3;

    public Transform target;
    public Soul soul;

    private DoorManager doors;
    private RoomTemplates room;

    // Start is called before the first frame update
    void Start()
    {
        stunned = false;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        doors = GetComponentInParent<DoorManager>();
        room = FindObjectOfType<RoomTemplates>();
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

        if(stunTime > 0){
            stunned = true;
            stunTime -= Time.deltaTime;
        }
        else   
            stunned = false;

        if(hp <= 0){
            //doors.killEnemy();
            room.getActiveRoom().GetComponent<DoorManager>().killEnemy();
            SpawnSouls(2, 4);
            Destroy(gameObject);
        }
    }

    private void SpawnSouls(int minSpawn, int maxSpawn)
    {
        var spawnNum = Random.Range(minSpawn, maxSpawn);
        for (var i = 0; i <= spawnNum; i++)
        {
            var randomPos = (Vector2)Random.insideUnitCircle * 5;
            if (!IsSoulColliding(randomPos))
            {
                randomPos += (Vector2)transform.position;
            }
            Instantiate(soul, randomPos, transform.rotation);
        }
    }

    // Needs a bit more testing, but I think this should work to not allow the souls to collide with walls.
    private bool IsSoulColliding(Vector2 position)
    {
        var colliders = Physics2D.OverlapCircleAll(position, 0.0f);
        return colliders.Length > 0;
    }

    public void Damage(int p)
    {
        if (!isInvincible)
        {
            hp -= p;
        }
    }

    public void Damage(int dmg, float stun) {
        if (stun > stunTime) {
            stunTime = stun;
        }
        Damage(dmg);
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
