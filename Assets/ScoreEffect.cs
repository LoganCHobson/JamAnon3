using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreEffect : MonoBehaviour
{
    private Animator anim;
    public UnityEvent onEffectComplete;

    private void Start()
    {
        anim.Play("ScoreEffectAnim");
    }

    public void AnimationComplete()
    {
        onEffectComplete.Invoke();
    }
}
