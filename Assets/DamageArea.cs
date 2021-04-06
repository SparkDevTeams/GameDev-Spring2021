using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField]
    protected int damage = 2;
    [SerializeField]
    protected float hitstun = 0.5f;
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
            }


            //Set Time Check to 0
            timeCheck = 0;
            
        }
    }
}
