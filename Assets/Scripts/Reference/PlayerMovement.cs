using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float playerVelocity;
    [SerializeField] private float speed;

    private bool movingRight;

    Rigidbody2D rigid;
   
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        rigid = GetComponent<Rigidbody2D>();
        movingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity = Input.GetAxisRaw("Horizontal") * speed;
    }

    private void FixedUpdate()
    {
        

        rigid.velocity = new Vector2(playerVelocity, rigid.velocity.y);

        if(playerVelocity > 0 && !movingRight)
        {
            flip();
        }
        else if(playerVelocity < 0 && movingRight)
        {
            flip();
        }

        
    }

    private void flip()
    {
        movingRight = !movingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
