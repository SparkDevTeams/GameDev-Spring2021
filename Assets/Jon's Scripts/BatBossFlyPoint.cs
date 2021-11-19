using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossFlyPoint : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private Vector2 flyDir;
    [SerializeField]
    private float followSpeed = 150.0f;
    [SerializeField]
    private bool followX = false;
    [SerializeField]
    private bool followY = false;
    [SerializeField]
    private bool pointToPlayer = false;
    private bool active = false;

    void Start()
    {
        active = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) 
        {
            if (followX) 
            {
                Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }

            if (followY)
            {
                Vector2 targetPosition = new Vector2(transform.position.x, target.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }
        }   
    }

    public void Activate() 
    {
        active = true;
    }

    public void Deactivate() 
    {
        active = false;
    }

    public Vector2 GetFlyDirection() 
    {
        Vector2 currFlyDirection = flyDir.normalized;

        if (pointToPlayer) 
        {
            currFlyDirection = ((Vector2)(target.position - transform.position)).normalized;
        }

        return currFlyDirection;
    }

    private void OnDrawGizmosSelected()
    {
        const float LINE_LENGTH = 5.0f;

        Debug.DrawLine((Vector2)transform.position, ((Vector2)transform.position + (flyDir.normalized * LINE_LENGTH)), Color.red);
    }
}
