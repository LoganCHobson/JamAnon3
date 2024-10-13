using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreEffect : MonoBehaviour
{
    private Animator anim;
    public UnityEvent onEffectComplete;
    public string animName;

    private void Start()
    {
        anim = GetComponent<Animator>();    
        anim.Play(animName);
    }

    public void AnimationComplete()
    {
        onEffectComplete.Invoke();
    }
}
