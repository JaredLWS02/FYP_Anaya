using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pilot))]
[RequireComponent(typeof(SideMove))]
[RequireComponent(typeof(Jump2D))]
[RequireComponent(typeof(WolfAI))]

public class Wolf : MonoBehaviour
{
    [HideInInspector]
    public Pilot pilot;
    SideMove move;
    Jump2D jump;
    [HideInInspector]
    public WolfAI ai;

    void Awake()
    {
        pilot = GetComponent<Pilot>();
        move = GetComponent<SideMove>();
        jump = GetComponent<Jump2D>();
        ai = GetComponent<WolfAI>();
    }

    // Event Manager ============================================================================

    void OnEnable()
    {
        EventManager.Current.MoveXEvent += MoveX;
        EventManager.Current.MoveYEvent += MoveY;
        EventManager.Current.JumpEvent += Jump;
        EventManager.Current.PlayerSwitchEvent += PlayerSwitch;

        PlayerManager.Current.Register(gameObject);
    }
    void OnDisable()
    {
        EventManager.Current.MoveXEvent -= MoveX;
        EventManager.Current.MoveYEvent -= MoveY;
        EventManager.Current.JumpEvent -= Jump;
        EventManager.Current.PlayerSwitchEvent -= PlayerSwitch;

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

    public bool AllowJump;
    public bool AllowSwitch;

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

        //if(!AllowMoveY) return;

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

    void PlayerSwitch(GameObject from, GameObject to)
    {
        if(!AllowSwitch) return;
        
        if(gameObject==from)
        {
            pilot.type = Pilot.Type.AI;
        }
        else if(gameObject==to)
        {
            pilot.type = Pilot.Type.Player;
        }
    }
}