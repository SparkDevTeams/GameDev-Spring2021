using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BatState
{
    IDLE,
    LUNGE,
    SHOOT,
    FLY,
    WALK
}

public enum BatDirection
{
    FRONT,
    BACK,
    LEFT,
    RIGHT
}

public class BatBossAnimator : MonoBehaviour
{
    

    [SerializeField]
    private Animator headAnimator, bodyAnimator;
    
    //Head Postions
    private Vector2 idleFront = new Vector2(0.0f, 2.25f);
    private Vector2 flyFront = new Vector2(0.0f, 2.0f);
    private Vector2 lungeFront = new Vector2(0.0f, 2.0f);
    private Vector2 idleSide = new Vector2(2.4f, 2.4f);
    private Vector2 flySide = new Vector2(2.4f, 2.8f);
    private Vector2 lungeSide = new Vector2(1.2f, 3.3f);
    private Vector2 idleBack = new Vector2(0.0f, 3.4f);
    private Vector2 lungeBack = new Vector2(0.0f, 3.3f);
    private Vector2 flyBack = new Vector2(0.0f, 4.3f);

    private Vector2 headAnchor = new Vector2(0.0f, 2.25f);

    //STATE BODY (MAIN)
    const string name= "BatBoss";
    private string bodyState = "Idle";
    private string bodyDirection = "Front";
    //State Head
    private string headState = "Idle";
    private string headDirection = "Front";
    // Start is called before the first frame update
    private float timecheck;
    
    public float motionspeedY = 1;
    public float motionspeedX = 1;
    public float Ysize = 1;
    public float Xsize = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timecheck += Time.deltaTime;
        float yoff = Mathf.Sin(timecheck * motionspeedY) * Ysize;
        float xoff = Mathf.Cos(timecheck * motionspeedX) * Xsize;

        headAnimator.gameObject.transform.localPosition = new Vector2(headAnchor.x + xoff, headAnchor.y + yoff);

        /*if (Input.GetButtonDown("Fire1")) {
            AnimationChange((BatState)Random.Range(0,5), (BatDirection)Random.Range(0, 4));
        }*/
    }

    public void AnimationChange( BatState state, BatDirection direction) {
        string bodyAnimation = name + "_";
        string headAnimation = "";

        switch (state) {
            case BatState.LUNGE:
               bodyAnimation = bodyAnimation + "Lunge";
               headAnimation = "Attack";

               if (direction == BatDirection.LEFT)
               {
                   transform.localScale = new Vector3(-1, 1, 1);
               }
               else {
                   transform.localScale = new Vector3(1, 1, 1);
               }


               switch (direction) {
                   case BatDirection.FRONT:
                       headAnimation = headAnimation + "_Front";
                       headAnchor = lungeFront;
                       bodyAnimation = bodyAnimation + "_Front";
                       break;
                    case BatDirection.BACK:
                        headAnimation = headAnimation + "_Back";
                        headAnchor = lungeBack;
                        bodyAnimation = bodyAnimation + "_Back";
                        break;
                    case BatDirection.RIGHT:
                        headAnimation = headAnimation + "_Side";
                        headAnchor = lungeSide;
                        bodyAnimation = bodyAnimation + "_Side";
                        break;
                    case BatDirection.LEFT:
                        headAnimation = headAnimation + "_Side";
                        headAnchor = lungeSide;
                        bodyAnimation = bodyAnimation + "_Side";
                        break;
                }
               
                break;
            case BatState.FLY:
                bodyAnimation = bodyAnimation + "Fly";
                headAnimation = "Attack";

                if (direction == BatDirection.LEFT)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }


                switch (direction)
                {
                    case BatDirection.FRONT:
                        headAnimation = headAnimation + "_Front";
                        headAnchor = flyFront;
                        bodyAnimation = bodyAnimation + "_Front";
                        break;
                    case BatDirection.BACK:
                        headAnimation = headAnimation + "_Back";
                        headAnchor = flyBack;
                        bodyAnimation = bodyAnimation + "_Back";
                        break;
                    case BatDirection.RIGHT:
                        headAnimation = headAnimation + "_Side";
                        headAnchor = flySide;
                        bodyAnimation = bodyAnimation + "_Side";
                        break;
                    case BatDirection.LEFT:
                        headAnimation = headAnimation + "_Side";
                        headAnchor = flySide;
                        bodyAnimation = bodyAnimation + "_Side";
                        break;
                }

                break;
            case BatState.SHOOT:

                bodyAnimation = bodyAnimation + "Idle";
                headAnimation = "Attack";

                if (direction == BatDirection.LEFT)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }


                switch (direction)
                {
                    case BatDirection.FRONT:
                        headAnimation = headAnimation + "_Front";
                        headAnchor = idleFront;
                        bodyAnimation = bodyAnimation + "_Front";
                        break;
                    case BatDirection.BACK:
                        headAnimation = headAnimation + "_Back";
                        headAnchor = idleBack;
                        bodyAnimation = bodyAnimation + "_Back";
                        break;
                    case BatDirection.RIGHT:
                        headAnimation = headAnimation + "_Side";
                        headAnchor = idleSide;
                        bodyAnimation = bodyAnimation + "_Side";
                        break;
                    case BatDirection.LEFT:
                        headAnimation = headAnimation + "_Side";
                        headAnchor = idleSide;
                        bodyAnimation = bodyAnimation + "_Side";
                        break;
                }

                break;

            case BatState.IDLE:
                bodyAnimation = bodyAnimation + "Idle";
                headAnimation = "Idle";

                if (direction == BatDirection.LEFT)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }


                switch (direction)
                {
                    case BatDirection.FRONT:
                        headAnimation = headAnimation + "_Front";
                        headAnchor = idleFront;
                        bodyAnimation = bodyAnimation + "_Front";
                        break;
                    case BatDirection.BACK:
                        headAnimation = headAnimation + "_Back";
                        headAnchor = idleBack;
                        bodyAnimation = bodyAnimation + "_Back";
                        break;
                    case BatDirection.RIGHT:
                        headAnimation = headAnimation + "_Side";
                        headAnchor = idleSide;
                        bodyAnimation = bodyAnimation + "_Side";
                        break;
                    case BatDirection.LEFT:
                        headAnimation = headAnimation + "_Side";
                        headAnchor = idleSide;
                        bodyAnimation = bodyAnimation + "_Side";
                        break;
                }
                break;

            case BatState.WALK:
                bodyAnimation = bodyAnimation + "Walk";
                headAnimation = "Idle";

                if (direction == BatDirection.LEFT)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }


                switch (direction)
                {
                    case BatDirection.FRONT:
                        headAnimation = headAnimation + "_Front";
                        headAnchor = idleFront;
                        bodyAnimation = bodyAnimation + "_Front";
                        break;
                    case BatDirection.BACK:
                        headAnimation = headAnimation + "_Back";
                        headAnchor = idleBack;
                        bodyAnimation = bodyAnimation + "_Back";
                        break;
                    case BatDirection.RIGHT:
                        headAnimation = headAnimation + "_Side";
                        headAnchor = idleSide;
                        bodyAnimation = bodyAnimation + "_Side";
                        break;
                    case BatDirection.LEFT:
                        headAnimation = headAnimation + "_Side";
                        headAnchor = idleSide;
                        bodyAnimation = bodyAnimation + "_Side";
                        break;
                }
                break;
        }
        
        bodyAnimator.Play(bodyAnimation);
        headAnimator.Play(headAnimation, 0);
    }
}
