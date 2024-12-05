using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] Animator animator;
    const string TransitionInString = "TransitionIn";
    const string TransitionOutString = "TransitionOut";

    private static bool _ignoreFirstTransition = false;

    public void Start()
    {
        if(!_ignoreFirstTransition)
        {
            _ignoreFirstTransition = true;
            return;
        }    
        SceneTransitionOut();
    }

    public void SceneTransitionIn()
    {
        animator.SetTrigger(TransitionInString);
    }

    public void SceneTransitionOut()
    {
        animator.SetTrigger(TransitionOutString);
    }
}
