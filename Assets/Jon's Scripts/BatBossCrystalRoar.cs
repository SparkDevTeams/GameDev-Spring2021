using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossCrystalRoar : MonoBehaviour
{
    const int EVENT_ID = 11;

    [SerializeField]
    private float totalCooldownTime = 2.0f;
    private float cooldownTimer = 0.0f;
    private bool roaring = false;

    void Start()
    {
        cooldownTimer = 0.0f;
        roaring = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (roaring) 
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0.0f) 
            {
                roaring = false;
            }
        }
    }

    public void Roar() 
    {
        roaring = true;
        cooldownTimer = totalCooldownTime;
        EventSystem.eventController.CrystalTrigger(11);
    }

    public bool IsRoaring() 
    {
        return roaring;
    }
}
