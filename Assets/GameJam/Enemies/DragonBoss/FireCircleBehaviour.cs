using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCircleBehaviour : MonoBehaviour
{
    public float totalDuration;
    public float currentDuration = 0;
    public float fadeDuration;
    float deleteFireTimer = 0;
    public float deleteFireDuration;
    public List<GameObject> fires = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.zero;

        //Animation

        currentDuration += Time.deltaTime;
        if (currentDuration >= fadeDuration)
        {
            deleteFireTimer += Time.deltaTime;

            if (deleteFireTimer > deleteFireDuration)
            {
                deleteFireTimer = 0;

                //Delete a random fire
                int rand = Random.Range(0, fires.Count);
                Destroy(fires[rand]);
                fires.RemoveAt(rand);
            }
        }

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
