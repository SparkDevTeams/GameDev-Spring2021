using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossShadow : MonoBehaviour
{
    [SerializeField]
    Transform body;
    [SerializeField]
    Vector3 shadowOffset;
    [SerializeField]
    Vector3 shadowMaxSize;
    public float shadowSizeMultiplier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = body.position + shadowOffset;
        shadowSizeMultiplier = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = body.position + shadowOffset;
    }

    void FixedUpdate()
    {
        transform.localScale = shadowMaxSize * shadowSizeMultiplier;
    }
}
