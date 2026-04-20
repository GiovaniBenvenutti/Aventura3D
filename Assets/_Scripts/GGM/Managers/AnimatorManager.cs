using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimationSteup> animationSetups;

    public enum AnimationType
    {
        IDLE,
        RUN,
        DEAD
    }

    public void Play (AnimationType type, float currentSpeedFactor = 1f) 
    {
        foreach (var animation in animationSetups)
        {
            if (animation.type == type)
            {
                animator.SetTrigger(animation.trigger);
                animator.speed = animation.speed;
                //Debug.Log("Playing animation: " + animation.trigger);
                break;
            }
        }
    }
}

[System.Serializable]
public class AnimationSteup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
    public float speed = 1f;
}
