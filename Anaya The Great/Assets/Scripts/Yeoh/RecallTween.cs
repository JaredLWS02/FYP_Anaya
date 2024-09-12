using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MnKMovement))]

public class RecallTween : MonoBehaviour
{
    MnKMovement movement;

    void Awake()
    {
        movement = GetComponent<MnKMovement>();
    }

    //---------------------------------------------------------------------------

    public KeyCode RecallKey = KeyCode.X;

    void Update()
    {
        if(Input.GetKeyDown(RecallKey))
        {
            Recall();
        }
    }

    bool canRecall = true;

    public Transform WolfTr;
    public float TweenTime = .2f;
    public float CooldownTime = 2;

    Tween recallTween;

    void Recall()
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
        canRecall = false;
        StartCoroutine(RecallCooling(CooldownTime));

        recallTween = WolfTr.DOMove(transform.position, TweenTime)
            .SetEase(Ease.InOutSine);
    }

    IEnumerator RecallCooling(float t)
    {
        yield return new WaitForSeconds(t);
        canRecall = true;
    }
}
