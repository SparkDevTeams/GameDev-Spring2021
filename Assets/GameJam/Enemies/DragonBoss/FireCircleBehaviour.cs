using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCircleBehaviour : MonoBehaviour
{
    public float totalDuration;
    public float currentDuration = 0;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.zero;

        //Animation

        currentDuration += Time.deltaTime;
        if (currentDuration >= totalDuration)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D (Collider2D hitInfo)
    {
        if(hitInfo.tag == "Player")
        {
            hitInfo.GetComponent<HealthManager>().damage(1 ,0.25f);           
        }
    }
}
