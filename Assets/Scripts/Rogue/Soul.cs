using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private int soulValue;
    public int minSoul;
    public int maxSoul;
    // Start is called before the first frame update
    void Start()
    {
        soulValue = Random.Range(minSoul, maxSoul);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().addSouls(soulValue);
            Destroy(gameObject);
        }
    }
}
