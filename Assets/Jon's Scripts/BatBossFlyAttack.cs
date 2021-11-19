using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossFlyAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private List<BatBossFlyPoint> flightPoints;
    [SerializeField]
    private List<int> flightPatterns;
    [SerializeField]
    private Vector2 localPositionOffset;
    [SerializeField]
    private float attackRadius = 25.0f;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private int damage = 7;
    [SerializeField]
    private float stunTime = 0.25f;
    [SerializeField]
    private float speed = 80.0f;
    [SerializeField]
    private float totalStartUpTime = 1.4f;
    [SerializeField]
    private float totalShortStartUpTime = 0.6f;
    [SerializeField]
    private float totalSweepTime = 1.8f;
    private float startUpTimer = 0.0f;
    private float sweepTimer = 0.0f;
    private int currPatternIndex = 0;
    private int currPointndex = 0;
    private bool flying = false;
    private bool startingUp = false;
    private bool changingPhases = false;
    private bool patternsActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flying = false;
        startingUp = false;
        changingPhases = false;
        patternsActive = false;
    }

    void Update()
    {
        if (flying) 
        {
            if (startingUp) 
            {
                startUpTimer -= Time.deltaTime;

                if(startUpTimer <= 0.0f) 
                {
                    startUpTimer = 0.0f;
                    sweepTimer = totalSweepTime;
                    startingUp = false;
                    changingPhases = true;
                }
            }
            else 
            {
                sweepTimer -= Time.deltaTime;

                if(sweepTimer <= 0.0f) 
                {
                    currPointndex++;

                    if(currPointndex >= flightPoints.Count || currPointndex >= flightPatterns[currPatternIndex + 1]) 
                    {
                        StopFlying();
                    }
                    else 
                    {
                        sweepTimer = 0.0f;
                        startUpTimer = totalStartUpTime;
                        startingUp = true;
                        changingPhases = true;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (flying)
        {
            if (startingUp && changingPhases)
            {
                rb.velocity = Vector2.zero;
                transform.position = flightPoints[currPointndex + flightPatterns[currPatternIndex]].transform.position;
                changingPhases = false;
            }

            if (!startingUp)
            {
                if (changingPhases)
                {
                    rb.velocity = flightPoints[currPointndex + flightPatterns[currPatternIndex]].GetFlyDirection() * speed;
                    changingPhases = false;
                }

                Attack();
            }
        }
    }

    public void StartFlying(int patternIndex) 
    {
        ActivatePatterns();

        if (patternIndex < 0)
        {
            currPatternIndex = 0;
        }
        else if (patternIndex >= flightPatterns.Count) 
        {
            currPatternIndex = flightPatterns.Count - 1;
        }
        else 
        {
            currPatternIndex = patternIndex;
        }

        currPointndex = 0;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        startUpTimer = totalShortStartUpTime;
        startingUp = true;
        changingPhases = true;
        flying = true;
    }

    public void StopFlying() 
    {
        flying = false;
        startingUp = false;
        changingPhases = false;
        startUpTimer = 0.0f;
        sweepTimer = 0.0f;
        currPatternIndex = 0;
        currPointndex = 0;
        rb.velocity = Vector2.zero;
        rb.isKinematic = false;
    }

    private void Attack() 
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position + (Vector3)localPositionOffset, attackRadius, playerLayer);

        if (playerCollider != null) 
        {
            playerCollider.gameObject.GetComponent<HealthManager>().damage(damage, stunTime);
        }
    }

    public bool IsFlying() 
    {
        return flying;
    }

    public int FlightPatternListSize() 
    {
        return flightPatterns.Count;
    }

    public void ActivatePatterns() 
    {
        if (!patternsActive) 
        {   
            foreach (BatBossFlyPoint point in flightPoints) 
            {
                point.Activate();
            }
            
        }

        patternsActive = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)localPositionOffset, attackRadius);
    }
}
