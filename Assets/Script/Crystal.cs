using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private const int EVENT_ID = 11;

    private EnemyManager manager;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private CircleCollider2D attackRadius;
    [SerializeField]
    private int damage = 2;
    [SerializeField]
    private float stunTime = 0.25f;
    [SerializeField]
    private float totalActiveTime = 3.0f;
    private float activeTimer = 0.0f;
    [SerializeField]
    private bool isInvincible = false;
    private bool isActive = false;


    void Start()
    {
        manager = GetComponent<EnemyManager>();
        activeTimer = 0.0f;
        isActive = false;

        if (isInvincible) 
        {
            manager.TriggerInvincibility();
        }

        EventSystem.eventController.onCrystalTrigger += TriggerCystalEvent;
    }

    void FixedUpdate()
    {
        if (isActive) 
        {
            activeTimer -= Time.deltaTime;

            if (activeTimer <= 0.0f)
            {
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
}
