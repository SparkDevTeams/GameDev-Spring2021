using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5;
    public float rollSpeed = 7.5f;
    private Animator animator;
    private SpriteRenderer sprite;
    private string direction = "Front";
    private string mode = "Idle";
    private float setTime = 0.0f;

    public string Direction {
        get { return direction; }
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (mode == "Roll" && setTime > 0) {
            if (direction == "Side")
            {
                x = rollSpeed * (transform.localScale.x * -1);
                y = 0;
            }
            else if (direction == "Back")
            {
                x = 0;
                y = rollSpeed;
            }
            else {
                x = 0;
                y = rollSpeed * -1;
            }

            rb.velocity = new Vector2(x * speed, y * speed);
            setTime -= Time.deltaTime;
            if (setTime <= 0)
            {
                mode = "Idle";
            }
            return;
        }

        if (mode != "Idle" && mode != "Walking") {
            if (Input.GetButtonDown("Fire1") && (setTime >= (0.267f * 0.6f)))
            {
                mode = "Attack";
                setTime = 0.267f;
                animator.Play("Mlafi_" + mode + "_" + direction, -1, 0.0f);
                return;
            }

            if (Input.GetButtonDown("Fire2") && (setTime >= (0.333f * 0.6f)))
            {
                mode = "Magic";
                setTime = 0.267f;
                animator.Play("Mlafi_" + mode + "_" + direction, -1, 0.0f);
                return;
            }

            setTime -= Time.deltaTime;
            if (setTime <= 0)
            {
                mode = "Idle";
            }
            return;
        }

       

        if (x > 0) {
            x = 1;
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if (y > 0) {
            direction = "Back";
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (y < 0) {
            direction = "Front";
            transform.localScale = new Vector3(1, 1, 1);
        } else if (x != 0) {
            direction = "Side";
            if (x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }


        if (Input.GetButtonDown("Fire1") && (mode == "Idle" || mode == "Walk")) {
            mode = "Attack";
            setTime = 0.267f;

            x = 0; y = 0;
        }

        if (Input.GetButtonDown("Fire2") && (mode == "Idle" || mode == "Walk"))
        {
            mode = "Magic";
            setTime = 0.333f;

            x = 0; y = 0;
        }

        if (Input.GetButtonDown("Fire3") && (mode == "Idle" || mode == "Walk"))
        {
            mode = "Roll";
            setTime = 0.333f;
            if (direction == "Side")
            {
                x = x * rollSpeed;
                y = 0;
            }
            else {
                x = 0;
                y = y * rollSpeed;
            }
            rb.velocity = new Vector2(x, y);
            animator.Play("Mlafi_" + mode + "_" + direction);
            return;
        }


        if (x != 0 && y != 0)
        {
            mode = "Walk";
            rb.velocity = new Vector2(x * 0.707f * speed, y * 0.707f * speed);
            animator.Play("Mlafi_" + mode + "_" + direction);
            return;
        }

        if (x != 0 || y != 0) {
            mode = "Walk";
        }


        rb.velocity = new Vector2(x * speed, y * speed);
        animator.Play("Mlafi_" + mode + "_" + direction);
    }
}
