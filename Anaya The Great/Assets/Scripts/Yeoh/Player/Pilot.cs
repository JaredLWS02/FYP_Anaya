using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class Pilot : MonoBehaviour
{
    public enum Type
    {
        None,
        Player,
        AI,
    }

    public Type type = Type.Player;  

    // Event Manager ============================================================================

    void OnEnable()
    {
        EventManager.Current.SwitchPilotEvent += SwitchPilot;
    }
    void OnDisable()
    {
        EventManager.Current.SwitchPilotEvent -= SwitchPilot;
    }

    // Events ============================================================================

    void SwitchPilot(GameObject who, Type to)
    {
        if(gameObject!=who) return;

        type = to;
    }

    //  ============================================================================

    void Update()
    {
        UpdateMoveInput();
    }

    // Input System ============================================================================

    Vector2 moveInput;

    void OnInputMove(InputValue value)
    {
        if(type!=Type.Player) return;

        moveInput = value.Get<Vector2>();
    }

    void UpdateMoveInput()
    {
        if(type!=Type.Player) moveInput=Vector2.zero;

        if(moveInput==Vector2.zero) return;

        EventManager.Current.OnTryMoveX(gameObject, moveInput.x);
        EventManager.Current.OnTryMoveY(gameObject, moveInput.y);
    }

    void OnInputJump(InputValue value)
    {
        if(type!=Type.Player) return;

        float input = value.Get<float>();

        EventManager.Current.OnTryJump(gameObject, input);
    }









    

    //  Old ============================================================================

    void OnInputSwitch()
    {
        if(type!=Type.Player) return;

        EventManager.Current.OnTrySwitch(gameObject);
    }

}
