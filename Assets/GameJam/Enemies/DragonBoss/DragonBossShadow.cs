using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossShadow : MonoBehaviour
{
    [SerializeField]
    Transform body;
    public Vector3 shadowOffset;
    [SerializeField]
    Vector3 shadowMaxSize;
    public float shadowSizeMultiplier = 1;
    public float shadowSizeTarget = 1;
    public float shadowSizeChangeDuration = 1;
    public bool fixedShadow = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = body.position + shadowOffset;
        shadowSizeMultiplier = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fixedShadow) 
            transform.position = body.position + shadowOffset;

        if (shadowSizeMultiplier != shadowSizeTarget)
        {
            shadowSizeMultiplier = Mathf.MoveTowards(shadowSizeMultiplier, shadowSizeTarget, (1 / shadowSizeChangeDuration) * Time.deltaTime);
        }

        transform.localScale = shadowMaxSize * shadowSizeMultiplier;
    }
}
