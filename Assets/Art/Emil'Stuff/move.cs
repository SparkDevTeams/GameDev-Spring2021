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
    private float setTime = 0.0f; //Cooldown

    public GameObject frontHitbox, sideHitbox, backHitbox;
    private GameObject hitbox;

    public string Direction {
        get { return direction; }
    }

    public string Mode {
        get { return mode; }
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetAnimation( string mode, float cooldown, bool stop = true) {
        this.mode = mode;
        setTime = cooldown;
        animator.Play("Mlafi_" + this.mode + "_" + direction);
        if (stop)
        {
            rb.velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == "Dead") {
            return;
        }

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
            if (Input.GetButtonDown("Fire1") && (setTime >= (0.267f * 0.6f)) && mode != "Hurt")
            {
                mode = "Attack";
                setTime = 0.267f;
                animator.Play("Mlafi_" + mode + "_" + direction, -1, 0.0f);
                StartCoroutine(Attack());
                return;
            }

            if (Input.GetButtonDown("Fire2") && (setTime >= (0.333f * 0.6f)) && mode != "Hurt")
            {
                Debug.Log("Can shoot: " + GetComponent<PlayerTest>().canShoot);
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
            StartCoroutine(Attack());
            x = 0; y = 0;
        }

        //if (Input.GetButtonDown("Fire2") && (mode == "Idle" || mode == "Walk"))
        //{
        //    Debug.Log("Can shoot: " + GetComponent<PlayerTest>().canShoot);
        //    mode = "Magic";
        //    setTime = 0.333f;

        //    x = 0; y = 0;
        //}

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

        if (mode == "Idle" || mode == "Walk") {
            if (x != 0 && y != 0)
            {
                mode = "Walk";
                rb.velocity = new Vector2(x * 0.707f * speed, y * 0.707f * speed);
                animator.Play("Mlafi_" + mode + "_" + direction);
                return;
            }

            if (x != 0 || y != 0)
            {
                mode = "Walk";
            }
        }


        if (mode != "Hurt")
        {
            rb.velocity = new Vector2(x * speed, y * speed);
        }
        animator.Play("Mlafi_" + mode + "_" + direction);
    }

    protected IEnumerator Attack() {
        if (hitbox != null) {
            Destroy(hitbox);
        }
        switch (direction) {
            case "Front":
                hitbox = Instantiate(frontHitbox, transform);
                break;
            case "Side":
                hitbox = Instantiate(sideHitbox, transform);
                break;
            case "Back":
                hitbox = Instantiate(backHitbox, transform);
                break;
        }
        yield return new WaitForSeconds(0.267f);
        Destroy(hitbox);
        hitbox = null;
    }
}
