using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectyle : MonoBehaviour
{

    private float timeActive = 0;
    [SerializeField]
    private float totalTime = 10; //Stay active for x seconds
    [SerializeField]
    private int damage = 3;
    [SerializeField]
    private float stuntime = 0.5f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeActive >= totalTime) {
            Destroy(gameObject);
        }
        timeActive += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            //Enemy HP Down
            //Enemy Stun slight
        }
    }
}
