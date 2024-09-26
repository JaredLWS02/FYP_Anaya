using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SideMove))]
[RequireComponent(typeof(Jump2D))]
[RequireComponent(typeof(AISidePathseeker))]

public class Wolf : MonoBehaviour
{
    SideMove move;
    Jump2D jump;
    AISidePathseeker seeker;

    void Awake()
    {
        move = GetComponent<SideMove>();
        jump = GetComponent<Jump2D>();
        seeker = GetComponent<AISidePathseeker>();
    }

    void FixedUpdate()
    {
        TryAIMove();
    }

    // ============================================================================

    [Header("Toggles")]
    public bool AllowMoveX;
    public bool AllowJump;
    
    public bool AllowSwitch;

    // ============================================================================

    public enum Control
    {
        None,
        Player,
        AI,
    }

    [Header("Control")]
    public Control control = Control.AI;
    public bool AllowPlayer;
    public bool AllowAI;

    // ============================================================================

    // input system
    void OnMove(InputValue value)
    {
        if(!AllowPlayer) return;

        Vector2 input_dir = value.Get<Vector2>();

        move.inputX = AllowMoveX ? input_dir.x : 0;
    }

    // ============================================================================

    // input system
    void OnJump(InputValue value)
    {
        if(!AllowPlayer) return;
        if(!AllowJump) return;

        float input = value.Get<float>();

        //press
        if(input>0)
        {
            jump.JumpBuffer();
        }
        //release
        else
        {
            jump.JumpCut();
        }
    }

    public bool IsGrounded()
    {
        return jump.IsGrounded();
    }

    // ============================================================================

    void TryAIMove()
    {
        if(!AllowAI) return;
        
        seeker.Move();
    }
}