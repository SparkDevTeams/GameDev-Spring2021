using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed = 3;

    //new dash
    bool dashButtonDown;
    private Vector3 moveDir; //to dash same direction character is facing
    public int dashAmount; //how far dash takes character


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0f; //horizontal
        float y = 0f; //vertical

        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            x = -1f; //left
        }

        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow)))
        {
            y = +1f; //up
        }

        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
        {
            x = +1f; //right
        }

        if (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow)))
        {
            y = -1f; //down
        }

        moveDir = new Vector3(x, y);
        //add player animation here

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashButtonDown = true;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * speed; //to move player

        if(dashButtonDown)
        {
            rb.MovePosition(transform.position + moveDir * dashAmount); //move the player for when dashing
            dashButtonDown = false; //true when player presses spacebar; can't press again while dashing
        }
    }
}
