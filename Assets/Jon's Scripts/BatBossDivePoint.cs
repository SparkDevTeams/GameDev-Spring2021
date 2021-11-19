using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossDivePoint : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 originalPos;
    private SpriteRenderer hitboxSprite;
    private Transform target;
    [SerializeField]
    private Color activeColor;
    [SerializeField]
    private float speed = 30.0f;
    private float targettingTimer = 0.0f;
    private bool active = false;
    private bool lockedPosition = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitboxSprite = GetComponent<SpriteRenderer>();
        TriggerInactiveSprite();
        originalPos = (Vector2)transform.position;
        active = false;
        lockedPosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) 
        {
            targettingTimer -= Time.deltaTime;

            if (targettingTimer <= 0.0f) 
            {
                lockedPosition = true;
                active = false;
                rb.velocity = Vector2.zero;
            }
        }
    }

    void FixedUpdate()
    {
        if (active) 
        {
            rb.velocity = ((Vector2)(target.position - transform.position)).normalized * speed;
        }
    }

    public void TriggerActiveSprite() 
    {
        hitboxSprite.color = activeColor;
    }

    public void TriggerInactiveSprite() 
    {
        hitboxSprite.color = Color.clear;
    }

    public Vector2 GetOriginalPosition() 
    {
        return originalPos;
    }

    public Vector2 GetCurrentPosition() 
    {
        return transform.position;
    }

    public void StartTargeting(Transform newTarget, float targettingTime) 
    {
        transform.position = originalPos;
        target = newTarget;
        targettingTimer = targettingTime;
        lockedPosition = false;
        active = true;
    }

    public bool IsPositionLocked() 
    {
        return lockedPosition;
    }
}
