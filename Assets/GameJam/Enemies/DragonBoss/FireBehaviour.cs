using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sr;

    string stateName;
    public int versionNum;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        stateName = "Fire_V" + versionNum;
    }

    void OnEnable()
    {
        float rand = Random.value;
        animator.Play(stateName, sr.sortingLayerID, rand);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
