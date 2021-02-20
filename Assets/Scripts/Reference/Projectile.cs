using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigid;

    public float projectileSpeed;
    public float elapsedTime;
    public const float MAX_TIME = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        //rigid.velocity = new Vector2(projectileSpeed, rigid.velocity.y);
        rigid.velocity = transform.right * projectileSpeed;
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= MAX_TIME)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("Hit!");
            collision.GetComponent<BatEnemy>().reduceHP();
            Destroy(gameObject);
        }
    }
}
