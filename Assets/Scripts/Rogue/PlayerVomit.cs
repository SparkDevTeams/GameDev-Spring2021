using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVomit : MonoBehaviour
{
    public GameObject vomitPrefab;
    public float vomitSpeed;
    public KeyCode buffActivationKey;
    public float buffDuration = 10f;

    private bool isBuffActive = false;
    private float buffTimer = 0f;

    private void Update()
    {
        if (Input.GetKeyDown(buffActivationKey))
        {
            StartBuffEffect();
        }

        if (isBuffActive)
        {
            buffTimer -= Time.deltaTime;

            if (buffTimer <= 0f)
            {
                EndBuffEffect();
            }
        }
    }

    private void StartBuffEffect()
    {
        if (isBuffActive)
        {
            return;
        }

        isBuffActive = true;
        buffTimer = buffDuration;

        StartCoroutine(ShootVomit());
    }

    private void EndBuffEffect()
    {
        isBuffActive = false;
    }

    private IEnumerator ShootVomit()
    {
        while (isBuffActive)
        {
            // Generate a random angle between 0 and 360 degrees
            float randomAngle = Random.Range(0f, 360f);
            Vector2 direction = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));

            GameObject vomit = Instantiate(vomitPrefab, transform.position, Quaternion.identity);
            Rigidbody2D vomitRB = vomit.GetComponent<Rigidbody2D>();
            vomitRB.velocity = direction * vomitSpeed;

            yield return new WaitForSeconds(0.5f); // Shoot vomit every 0.5 seconds
        }
    }
}
