using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapMovement : MonoBehaviour
{
    [SerializeField]
    private float DISTANCE_MARGIN = 0.25f;
    [SerializeField]
    private float RUN_MARGIN = 3.5f;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 3.5f;
    [SerializeField]
    private float runMult = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Analyze for attack

        //Move
        if (Input.GetMouseButton(0))
        {
            Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            float distance = Mathf.Sqrt(Mathf.Pow(( worldPos.y - transform.position.y), 2) + Mathf.Pow((worldPos.x - transform.position.x), 2));
            if (Mathf.Abs(distance) > DISTANCE_MARGIN) {
                //Detect Angle
                float x = (worldPos.x - transform.position.x) / distance;
                float y = (worldPos.y - transform.position.y) / distance;

                if (Mathf.Abs(distance) > RUN_MARGIN) {
                    x *= runMult; 
                    y *= runMult;
                }

                rb.velocity = new Vector2(x * speed, y * speed);
            } else {
                rb.velocity = Vector2.zero;
            }
            return;
        }
        
        //Stop Moving if Not pressing Screen
            rb.velocity = Vector2.zero;
        
    }
}
