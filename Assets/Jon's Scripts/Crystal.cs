using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private const int EVENT_ID = 11;

    private EnemyManager manager;
    [SerializeField]
    private SpriteRenderer hitBoxSprite;
    [SerializeField]
    private Color activeColor;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private CircleCollider2D attackRadius;
    [SerializeField]
    private int startHp = 1;
    private int hp;
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float stunTime = 0.3f;
    [SerializeField]
    private float totalActiveTime = 3.0f;
    private float activeTimer = 0.0f;
    [SerializeField]
    private bool isInvincible = false;
    private bool isActive = false;


    void Start()
    {
        manager = GetComponent<EnemyManager>();
        hp = startHp;
        activeTimer = 0.0f;
        isActive = false;

        if (isInvincible) 
        {
            manager.TriggerInvincibility();
        }

        TriggerInactiveSprite();
        EventSystem.eventController.onCrystalTrigger += TriggerCystalEvent;
    }

    void FixedUpdate()
    {
        if (isActive) 
        {
            activeTimer -= Time.deltaTime;

            if (activeTimer <= 0.0f)
            {
                TriggerInactiveSprite();
                isActive = false;
                activeTimer = 0.0f;
            }
            else
            {
                PlayerDamageScan();
            }
        }
    }

    public void TriggerCystalEvent(int id) 
    {
        if (id == EVENT_ID)
        {
            TriggerActiveSprite();
            activeTimer = totalActiveTime;
            isActive = true;
        }
    }

    public void DestroyCrystal() 
    {
        Destroy(gameObject);
    }

    private void PlayerDamageScan() 
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(attackRadius.transform.position, attackRadius.radius, playerLayer);

        if (playerCollider != null)
        {
            playerCollider.gameObject.GetComponent<HealthManager>().damage(damage, stunTime);
        }
    }

    private void TriggerActiveSprite() 
    {
        if (hitBoxSprite != null)
        {
            hitBoxSprite.color = activeColor;
        }
    }

    private void TriggerInactiveSprite() 
    {
        if (hitBoxSprite != null)
        {
            hitBoxSprite.color = Color.clear;
        }
    }

    public void Hit() 
    {
        if (!isInvincible)
        {
            hp--;

            if (hp <= 0)
            {
                DestroyCrystal();
            }
        }
    }
}
