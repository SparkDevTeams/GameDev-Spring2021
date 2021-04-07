using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField]
    protected int damage = 2;
    [SerializeField]
    protected float hitstun = 0.1f;
    [SerializeField]
    protected float dmgFreq = 0.5f;
    protected float timeCheck = 0;
    [SerializeField]
    protected float radius = 0.5f;

    private void Update()
    {
        timeCheck += Time.deltaTime;
        if (timeCheck >= dmgFreq) {
            RaycastHit2D[] hit2Ds = Physics2D.CircleCastAll(gameObject.transform.localPosition, radius, Vector2.zero );
            //cast circle
            //check tags
            //deal damage
            foreach (RaycastHit2D r in hit2Ds) {
                Debug.Log("SandPit hit : " + r.collider.gameObject.name);
                if (r.collider.gameObject.tag == "Enemy" && (tag != "Enemy" || tag== "Hazard")) {
                    EnemyManager em = r.collider.gameObject.GetComponent<EnemyManager>();
                    if (em != null) {
                        em.stunTime = hitstun;
                        em.Damage(damage);
                    }
                }

                if (r.collider.gameObject.tag == "Player" && tag == "Hazard") {
                    HealthManager hm = r.collider.gameObject.GetComponent<HealthManager>();
                    if (hm != null) {
                        hm.damage(damage, hitstun);
                    }
                }

            }
            //Set Time Check to 0
            timeCheck = 0;
            
        }
    }
}
