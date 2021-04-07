using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float stunTime;
    public bool stunned;

    public int hp, startHp = 3;

    public Transform target;

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
            Destroy(gameObject);
        }
    }

    public void Damage(int p){

        hp -= p;
    }
}
