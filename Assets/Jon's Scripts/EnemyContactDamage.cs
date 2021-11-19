using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField]
    private float stunTime = 0.25f;
    [SerializeField]
    private int contactDamage = 4;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthManager>().damage(contactDamage, stunTime);
        }
    }
}
