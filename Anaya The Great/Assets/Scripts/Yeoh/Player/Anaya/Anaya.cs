using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pilot))]
[RequireComponent(typeof(SideMove))]
[RequireComponent(typeof(Jump2D))]
[RequireComponent(typeof(AnayaAI))]

public class Anaya : MonoBehaviour
{
    [HideInInspector]
    public Pilot pilot;
    SideMove move;
    Jump2D jump;
    [HideInInspector]
    public AnayaAI ai;

    void Awake()
    {
        pilot = GetComponent<Pilot>();
        move = GetComponent<SideMove>();
        jump = GetComponent<Jump2D>();
        ai = GetComponent<AnayaAI>();
    }

    // Event Manager ============================================================================

    void OnEnable()
    {
        EventManager.Current.MoveXEvent += MoveX;
        EventManager.Current.MoveYEvent += MoveY;
        EventManager.Current.JumpEvent += Jump;
        EventManager.Current.TrySwitchEvent += TrySwitch;

        PlayerManager.Current.Register(gameObject);
    }
    void OnDisable()
    {
        EventManager.Current.MoveXEvent -= MoveX;
        EventManager.Current.MoveYEvent -= MoveY;
        EventManager.Current.JumpEvent -= Jump;
        EventManager.Current.TrySwitchEvent -= TrySwitch;

        PlayerManager.Current.Unregister(gameObject);
    }

    // ============================================================================

    void Start()
    {
        EventManager.Current.OnSpawn(gameObject);
    }

    void Update()
    {
        TryStopMove();
    }

    // Actions ============================================================================

    [Header("Toggles")]
    public bool AllowMoveX;
    public bool AllowMoveY;
    
    public bool AllowJump;
    public bool AllowDash;
    public bool AllowClimb;
    public bool AllowCrawl;
    public bool AllowStand;
    public bool AllowSwitch;
    public bool AllowCommand;

    // ============================================================================

    void MoveX(GameObject mover, float input_x)
    {
        if(mover!=gameObject) return;

        if(!AllowMoveX) return;

        move.dirX = input_x;
    }

    void MoveY(GameObject mover, float input_y)
    {
        if(mover!=gameObject) return;

        if(!AllowMoveY) return;

        //climb.inputY = input_y;
    }

    void TryStopMove()
    {
        if(pilot.type == Pilot.Type.None || !AllowMoveX)
        move.dirX = 0;

        //if(pilot.type == Pilot.Type.None || !AllowMoveY)
        //climb.inputY = 0;
    }
    
    // ============================================================================

    void Jump(GameObject jumper, float input)
    {
        if(jumper!=gameObject) return;

        if(!AllowJump) return;

        if(input>0) //press
        {
            jump.JumpBuffer();
        }
        else //release
        {
            jump.JumpCut();
        }
    }

    public bool IsGrounded()
    {
        return jump.IsGrounded();
    }

    // ============================================================================

    void TrySwitch(GameObject switcher)
    {
        if(switcher!=gameObject) return;
        if(!AllowSwitch) return;
        
        PlayerManager.Current.Switch(gameObject);
    }

    // ============================================================================

    public bool IsCrawling()
    {
        return false;
    }

}
