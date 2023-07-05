using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    public bool falling;
    public float targetPosY;
    public float speed;
    public float time;
    public GameObject fireCircle;
    public GameObject shadowObj;
    private DragonBossShadow shadow;
    public bool haveShadow;
    private bool started = false;

    private void Start()
    {
        shadow = shadowObj.GetComponent<DragonBossShadow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            started = true;

            if (haveShadow)
            {
                shadow.transform.position = new Vector3(transform.position.x, targetPosY, transform.position.z);
                shadow.shadowSizeMultiplier = 0.1f;
                shadow.shadowSizeTarget = 1;
                shadow.shadowSizeChangeDuration = time * 0.75f;
            }
            else
            {
                shadowObj.SetActive(false);
            }
        }        

        //Animation

        if (falling)
        {
            //Fireball falling
            transform.position -= new Vector3(0, Time.deltaTime * speed, 0);
            if (transform.position.y < targetPosY)
            {
                transform.position = new Vector3(transform.position.x, targetPosY, transform.position.z);

                //Create fire on ground
                Instantiate(fireCircle, transform.position, Quaternion.identity);

                //Destroy fireball
                Destroy(this.gameObject);                
            }

            if (haveShadow)
            {
                shadow.transform.position = new Vector3(transform.position.x, targetPosY, transform.position.z);
            }
        }
        else
        {
            //Fireball rising
            transform.position += new Vector3(0, Time.deltaTime * speed, 0);
            if (transform.position.y > targetPosY)
            {
                //Destroy fireball
                Destroy(this.gameObject);
            }
        }
    }
}
