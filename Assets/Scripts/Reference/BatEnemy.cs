using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public int enemyHealth;

    public Transform node1;
    public Transform node2;
    public Transform currentNode;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        currentNode = node2;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, currentNode.position, speed * Time.deltaTime);

        if(transform.position == node1.position)
        {
            currentNode = node2;
        }
        else if(transform.position == node2.position)
        {
            currentNode = node1;
        }

    }

    public void reduceHP()
    {
        enemyHealth -= 1;
    }
}
