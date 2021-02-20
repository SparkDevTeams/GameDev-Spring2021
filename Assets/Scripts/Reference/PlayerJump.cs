using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private float jumpHeight;
    private bool jumping;
    public bool grounded;
    private float groundRadius;

    Rigidbody2D rigid;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    // Start is called before the first frame update
    void Start()
    {
        jumpHeight = 15f;
        jumping = false;
        rigid = GetComponent<Rigidbody2D>();
        groundRadius = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (jumping)
        {
            jump();
            jumping = false;
        }
    }

    void jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpHeight);
    }

    public void setJumped(bool jumped)
    {
        this.jumping = jumped;
    }
}
