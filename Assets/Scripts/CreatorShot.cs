using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorShot : PlayerProjectyle
{
    [SerializeField]
    protected bool createOnDestroy = true;
    [SerializeField]
    protected bool destroyOnCreate = true;
    [SerializeField]
    protected bool createOnHit = true;
    [SerializeField]
    protected GameObject creation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDirection();
    }
    void Update()
    {
        if (timeActive >= totalTime)
        {
            if (createOnDestroy) {
                create();
            }
            Destroy(gameObject);
        }

        timeActive += Time.deltaTime;
    }

    protected virtual void create() {
        GameObject obj = Instantiate(creation);
        obj.transform.localPosition = (transform.localPosition);

        if (createOnDestroy && (timeActive >= totalTime)) {
            return;
        }
        if (destroyOnCreate) {
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Enemy HP Down
            //Enemy Stun slight
            if (createOnHit) {
                create();
            }

            if (destroyOnHit && !destroyOnCreate)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            if (createOnHit)
            {
                create();
            }

            if (destroyOnHit && !destroyOnCreate)
            {
                Destroy(gameObject);
            }
        }
    }
}
