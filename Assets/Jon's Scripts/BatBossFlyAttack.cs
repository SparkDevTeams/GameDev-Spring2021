using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossFlyAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyManager manager;
    private BatBossAnimator animator;
    [SerializeField]
    private List<BatBossFlyPoint> flightPoints;
    [SerializeField]
    private BatBossDivePoint divePoint;
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
    private int currPointIndex = 0;
    private bool flying = false;
    private bool startingUp = false;
    private bool changingPhases = false;
    private bool patternsActive = false;
    private bool diving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GetComponent<EnemyManager>();
        animator = GetComponent<BatBossAnimator>();
        flying = false;
        startingUp = false;
        changingPhases = false;
        patternsActive = false;
        diving = false;
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
                    currPointIndex++;
                    int nextPatternIndex = currPatternIndex + 1;
                    bool lastIndex = nextPatternIndex >= flightPatterns.Count;            
                                 

                    if(currPointIndex >= flightPoints.Count || (!lastIndex && currPointIndex >= flightPatterns[ currPatternIndex + 1])) 
                    {
                        Dive();
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

        if (diving)
        {
            if (divePoint.IsPositionLocked())
            {
                transform.position = divePoint.GetCurrentPosition();
                diving = false;
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
                transform.position = flightPoints[currPointIndex].transform.position;
                UpdateAnimation();
                changingPhases = false;
            }

            if (!startingUp)
            {
                if (changingPhases)
                {
                    rb.velocity = flightPoints[currPointIndex].GetFlyDirection() * speed;
                    changingPhases = false;
                }

                Attack();
            }
        }
    }

    public void StartFlying(int patternIndex) 
    {
        GetComponent<BatBossManager>().attacking = true;
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

        currPointIndex = flightPatterns[currPatternIndex];
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        startUpTimer = totalShortStartUpTime;
        startingUp = true;
        changingPhases = true;
        flying = true;
    }

    private void Dive() 
    {
        StopFlying();
        diving = true;
        divePoint.StartTargeting(manager.target, 1.0f);
    }

    public void StopFlying() 
    {
        flying = false;
        startingUp = false;
        changingPhases = false;
        startUpTimer = 0.0f;
        sweepTimer = 0.0f;
        currPatternIndex = 0;
        currPointIndex = 0;
        rb.velocity = Vector2.zero;
        rb.isKinematic = false;
        GetComponent<BatBossManager>().attacking = false;
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
        return flying || diving;
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

    private void UpdateAnimation() 
    {
        const float FULL_CIRCLE = 360.0f;

        float angle = Mathf.Atan2(flightPoints[currPointIndex].GetFlyDirection().y, flightPoints[currPointIndex].GetFlyDirection().x) * Mathf.Rad2Deg;

        if (angle < 0.0f)
        {
            angle += FULL_CIRCLE;
        }

        if (angle <= 45.0f || angle > 315.0f)
        {
            animator.AnimationChange(BatState.FLY, BatDirection.RIGHT);
        }
        else if (angle > 45.0f && angle <= 135.0f)
        {
            animator.AnimationChange(BatState.FLY, BatDirection.BACK);
        }
        else if (angle > 135.0f && angle <= 225.0f)
        {
            animator.AnimationChange(BatState.FLY, BatDirection.LEFT);
        }
        else 
        {
            animator.AnimationChange(BatState.FLY, BatDirection.FRONT);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)localPositionOffset, attackRadius);
    }
}
