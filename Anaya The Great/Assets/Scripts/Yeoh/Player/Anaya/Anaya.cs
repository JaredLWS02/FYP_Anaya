using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SideMove))]
[RequireComponent(typeof(Jump2D))]

public class Anaya : MonoBehaviour
{
    SideMove move;
    Jump2D jump;

    void Awake()
    {
        move = GetComponent<SideMove>();
        jump = GetComponent<Jump2D>();
    }

    // ============================================================================

    [Header("Actions")]
    public bool AllowMoveX;
    public bool AllowMoveY;
    public bool AllowJump;
    public bool AllowDash;
    public bool AllowClimb;
    public bool AllowCrawl;
    public bool AllowStand;
    public bool AllowSwitch;
    public bool AllowCommand;

    [Header("Control")]
    public bool AllowPlayer;
    public bool AllowAI;

    // ============================================================================

    // input system
    void OnMove(InputValue value)
    {
        Vector2 input_dir = value.Get<Vector2>();

        move.inputX = AllowMoveX ? input_dir.x : 0;
        //climb.inputY = AllowMoveY ? input_dir.y : 0;
    }

    // input system
    void OnJump(InputValue value)
    {
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
}
