using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float playerVelocityX;
    [SerializeField]private float playerVelocityY;
    private float speed;

    private Rigidbody2D rigid;
    private Vector2 movement;
    private Vector2 mousePos;
    private Animator anim;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>(); 
        speed = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //Moves player lol
        //Debug.Log("moving");

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(movement.x != 0 || movement.y != 0)
        {
            //anim.SetBool("isMoving", true);
        }
        else
        {
            //anim.SetBool("isMoving", false);
        }

    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + movement * speed * Time.fixedDeltaTime);

        //Vector2 lookDir = mousePos - rigid.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rigid.rotation = angle;
    }
}
