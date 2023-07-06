using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWarningScript : MonoBehaviour
{
    public SpriteRenderer rend;
    public float flashDuration = 0.5f;
    public float flashInterval = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private System.Collections.IEnumerator Flash()
    {
        while (true)
        {
            rend.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            rend.color = Color.white;
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
