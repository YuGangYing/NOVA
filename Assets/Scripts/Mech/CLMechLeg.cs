using UnityEngine;
using System.Collections;

[AddComponentMenu("Nova/Mech/Leg")]

public class CLMechLeg : MonoBehaviour
{
    public Transform BodyLink;
    public AnimationClip IdleAnimClip;
    public AnimationClip RunAnimClip;
    public AnimationClip AttackAnimClip;
    public int PrefabID;

    void Awake()
    {
        CLAnimation.AddAnimationClip(GetComponent<Animation>(), IdleAnimClip, WrapMode.Loop, 0);
        CLAnimation.AddAnimationClip(GetComponent<Animation>(), RunAnimClip, WrapMode.Loop, 0);
        CLAnimation.AddAnimationClip(GetComponent<Animation>(), AttackAnimClip, WrapMode.Loop, 0);
		if(RunAnimClip!=null)
		GetComponent<Animation>().Play(RunAnimClip.name);
    }
}