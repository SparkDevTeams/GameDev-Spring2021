using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    public bool falling;
    public float targetPosY;
    public float speed;
    public GameObject fireCircle;
    public GameObject shadow;
    public bool haveShadow;

    // Update is called once per frame
    void Update()
    {
        if (!haveShadow && shadow.activeSelf)
        {
            shadow.SetActive(false);
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
