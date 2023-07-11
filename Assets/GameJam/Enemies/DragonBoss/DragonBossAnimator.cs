using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragonState
{
    FIREBALL,
    FLY,
    LASER,
    MOVE,
    STOMP,
    TAIL
}

public enum DragonDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    ANY,
    NONE
}

public class DragonBossAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    //STATE
    const string bossName= "BossDragon";
    private string state = "Flying";
    public DragonDirection dragonDirection = DragonDirection.LEFT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AnimationChange( DragonState state, DragonDirection direction, float playbackSpeed = 1, float angle = 0) {
        animator.speed = playbackSpeed;

        dragonDirection = direction;
        string animationString = bossName;

        switch (state)
        {
            case DragonState.FIREBALL:
                animationString += "Fireball";
                break;
            
            case DragonState.FLY:
                animationString += "Flying";

                if (direction == DragonDirection.ANY)
                    transform.localRotation = Quaternion.Euler(0, 0, angle);

                break;
            
            case DragonState.LASER:
                animationString += "Laser";
                break;
            
            case DragonState.MOVE:
                animationString += "Moving";
                break;
            
            case DragonState.STOMP:
                animationString += "Stomp";
                break;
            
            case DragonState.TAIL:
                animationString += "Homing";
                break;
        }        

        if (direction == DragonDirection.RIGHT)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (direction == DragonDirection.UP)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (direction == DragonDirection.DOWN)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction != DragonDirection.ANY)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        
        animator.Play(animationString);
    }
}
