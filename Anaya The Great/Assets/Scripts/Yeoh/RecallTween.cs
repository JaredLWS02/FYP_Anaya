using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MnKMovement))]

public class RecallTween : MonoBehaviour
{
    MnKMovement movement;

    void Awake()
    {
        movement = GetComponent<MnKMovement>();
    }

    //---------------------------------------------------------------------------

    bool canRecall = true;

    public Transform wolfTr;
    public float tweenTime = .2f;
    public float cooldownTime = 2;

    Tween recallTween;

    // new input system event
    void OnRecall()
    {
        // ignore if still tweening
        if(recallTween!=null && recallTween.IsActive())
        {
            //recallTween.Kill();   // this is for spam proof
            return;
        }

        // ignore if not grounded 
        if(!movement.IsGrounded()) return;

        // ignore if cooling down
        if(!canRecall) return;
        StartCoroutine(RecallCooling(cooldownTime));

        recallTween = wolfTr.DOMove(transform.position, tweenTime)
            .SetEase(Ease.InOutSine);
    }

    IEnumerator RecallCooling(float t)
    {
        canRecall = false;
        yield return new WaitForSeconds(t);
        canRecall = true;
    }
}
