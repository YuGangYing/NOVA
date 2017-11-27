using UnityEngine;
using System.Collections;

public class CLAnimation
{
    public static void CrossFade(Animation animationComponent, AnimationClip animationClip, float fadeLength)
    {
        if (animationComponent != null && animationClip != null)
        {
            animationComponent.CrossFade(animationClip.name, fadeLength);
        }
    }

    public static void AddAnimationClip(Animation animationComponent, AnimationClip animationClip, WrapMode wrapMode, int layer)
    {
        if (animationComponent != null && animationClip != null)
        {
            animationComponent.AddClip(animationClip, animationClip.name);
            animationComponent[animationClip.name].wrapMode = wrapMode;
            animationComponent[animationClip.name].layer = layer;
        }
    }

    public static void SetAnimationSpeed(Animation animationComponent, AnimationClip animationClip, float speed)
    {
        if (animationComponent != null && animationClip != null)
        {
            animationComponent[animationClip.name].speed = speed;
        }
    }
}