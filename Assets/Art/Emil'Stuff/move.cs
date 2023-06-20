using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 6.5f;
    [SerializeField]
    private float rollSpeed = 24.0f;
    [SerializeField]
    private float dodgeSpeed = 24.0f;
    private Animator animator;
    private SpriteRenderer sprite;
    private string direction = "Front";
    private string mode = "Idle";
    private float setTime = 0.0f; //Cooldown
    private bool rollStart = false;

    private float x = 0, y = 0;

    private Vector2 rollVector =  new Vector2(0, 0);

    public GameObject frontHitbox, sideHitbox, backHitbox;
    private GameObject hitbox;
    private PlayerStats stats;

    public GameObject bagUI;
    private bool bagIsOpen = false;
    public GameObject pausedUI;
    public static bool gameIsPaused = false;

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

        x = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(x) > 0.3f)
        {
            if (x < 0)
            {
                x = -1;
            }
            else if (x > 0)
            {
                x = 1;
            }
            //Correct X
        }
        else { x = 0; }

        y = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(y) > 0.3f)
        {
            if (y < 0)
            {
                y = -1;
            }
            else if (y > 0)
            {
                y = 1;
            } //Correct Y
        }
        else { y = 0; }


        if (gameIsPaused == false && mode != "Roll" && !Input.GetButton("Fire3")) {
            rollStart = false;
        }
        
        //Roll Execute
        if (gameIsPaused == false && mode == "Roll" && setTime > 0) {
            /*if (direction == "Side")
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

            rb.velocity = new Vector2(x * speed, y * speed);*/
            rb.velocity = rollVector;
            setTime -= Time.deltaTime;
            if (setTime <= 0)
            {
                mode = "Idle";
                rb.velocity = Vector2.zero;
            }
            return;
        }


        if (gameIsPaused == false && mode != "Idle" && mode != "Walking") 
        {
            if (Input.GetButtonDown("Fire1") && (setTime <= (0.267f / 8.0f * 4.0f)) && mode != "Hurt")
            {
                mode = "Attack";
                setTime = 0.267f;
                animator.Play("Mlafi_" + mode + "_" + direction, -1, 0.0f);
                StartCoroutine(Attack());
                return;
            }//Cancel into attack

            if (Input.GetButtonDown("Fire2") && (setTime >= (0.333f * 0.6f)) && mode != "Hurt")
            {
                Debug.Log("Can shoot: " + GetComponent<PlayerTest>().canShoot);
                mode = "Magic";
                setTime = 0.267f;
                animator.Play("Mlafi_" + mode + "_" + direction, -1, 0.0f);
                return;
            }//Cancel into magic

            setTime -= Time.deltaTime;
            if (setTime <= 0)
            {
                mode = "Idle";
            }
            return;
        }

        if (gameIsPaused == false && Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleBag();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        //Set Direction
        changeDirection();

        if (gameIsPaused == false && Input.GetButtonDown("Fire1") && (mode == "Idle" || mode == "Walk")) {
            mode = "Attack";
            setTime = 0.267f;
            StartCoroutine(Attack());
            x = 0; y = 0;
            //return;
        }

        //if (Input.GetButtonDown("Fire2") && (mode == "Idle" || mode == "Walk"))
        //{
        //    Debug.Log("Can shoot: " + GetComponent<PlayerTest>().canShoot);
        //    mode = "Magic";
        //    setTime = 0.333f;

        //    x = 0; y = 0;
        //}

        //ROLL SET
        if (gameIsPaused == false && Input.GetButton("Fire3")&& !rollStart && (mode == "Idle" || mode == "Walk"))
        {
            mode = "Roll";
            setTime = 0.222f;

            if (x == 0 && y == 0) {
                if (direction == "Side")
                {
                    if (transform.localScale.x < 0)
                    {
                        x = 1;
                    }
                    else if (transform.localScale.x > 0) {
                        x = -1;
                    }
                }
                else
                {
                    if (direction == "Back")
                    {
                        y = 1;
                    }
                    else if (direction == "Front")
                    {
                        y = -1;
                    }
                    else {
                        rollStart = true; // Just in case
                        return;
                    }
                   
                }
            }

            rollVector.x = rollSpeed * x;
            rollVector.y = rollSpeed * y ;
            if (x != 0 && y != 0) {
                rollVector.x *= 0.707f;
                rollVector.y *= 0.707f;
            }//For Diagonal

            rollStart = true;
            rb.velocity = rollVector;
            animator.Play("Mlafi_" + mode + "_" + direction);
            return;
        }

        //WALK
        if (gameIsPaused == false && mode == "Idle" || mode == "Walk")
        {
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


        if (gameIsPaused == false && mode != "Hurt")
        {
            rb.velocity = new Vector2(x * speed, y * speed);
        }

        animator.Play("Mlafi_" + mode + "_" + direction);
    }

    public void ToggleBag()
    {
        if (bagIsOpen == true)
        {
            bagUI.SetActive(false);
            bagIsOpen = false;
        }
        else
        {
            bagUI.SetActive(true);
            bagIsOpen = true;
        }
    }

    public void TogglePause()
    {
        if (gameIsPaused == true)
        {
            pausedUI.SetActive(false);
            gameIsPaused = false;
            Time.timeScale = 1f;
        }
        else
        {
            pausedUI.SetActive(true);
            gameIsPaused = true;
            Time.timeScale = 0f;
        }
    }

    private void changeDirection() {
        if (gameIsPaused == false && x > 0)
        {
            x = 1;
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if (gameIsPaused == false && y != 0)
        {
            if (direction == "Side" && x != 0)
            {
                direction = "Side";
                if (x < 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                if (y < 0)
                {
                    direction = "Front";
                }
                else
                {
                    direction = "Back";
                }
                transform.localScale = new Vector3(1, 1, 1);
            }

        }
        /*else if (y < 0) {
            direction = "Front";
            transform.localScale = new Vector3(1, 1, 1);
        }*/
        else if (gameIsPaused == false && x != 0)
        {
            direction = "Side";
            if (x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    protected IEnumerator Attack() {
        rb.velocity = Vector2.zero;
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

        while (setTime > 0) {
            //Cancel into attack
            //Already in update


            //Cancel into dodge roll
            if (gameIsPaused == false && Input.GetButton("Fire3") && !rollStart && setTime < (0.067f))
            {

                mode = "Roll";
                setTime = 0.111f;

                if (x == 0 && y == 0)
                {
                    if (direction == "Side")
                    {
                        if (transform.localScale.x < 0)
                        {
                            x = 1;
                        }
                        else if (transform.localScale.x > 0)
                        {
                            x = -1;
                        }
                    }
                    else
                    {
                        if (direction == "Back")
                        {
                            y = 1;
                        }
                        else if (direction == "Front")
                        {
                            y = -1;
                        }
                        else
                        {
                            rollStart = true; // Just in case
                            Destroy(hitbox);
                            hitbox = null;
                            yield break;
                        }

                    }
                }

                rollVector.x = dodgeSpeed * x;
                rollVector.y = dodgeSpeed * y;
                if (x != 0 && y != 0)
                {
                    rollVector.x *= 0.707f;
                    rollVector.y *= 0.707f;
                }//For Diagonal

                rollStart = true;
                Destroy(hitbox);
                hitbox = null;
                changeDirection();
                rb.velocity = rollVector;
                animator.Play("Mlafi_" + mode + "_" + direction);
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
        //yield return new WaitForSeconds(0.267f);
        Destroy(hitbox);
        hitbox = null;
    }

    public void addSpeed(float speedUp)
    {
        speed += speedUp;
    }
}
