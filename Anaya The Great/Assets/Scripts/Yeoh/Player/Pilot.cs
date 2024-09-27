using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class Pilot : MonoBehaviour
{
    // Event Manager ============================================================================

    void OnEnable()
    {
        EventManager.Current.SwitchEvent += SwitchPilot;
    }
    void OnDisable()
    {
        EventManager.Current.SwitchEvent -= SwitchPilot;
    }

    // ============================================================================

    public enum Type
    {
        None,
        Player,
        AI,
    }

    public Type type = Type.Player;    

    // Input System ============================================================================

    void OnMove(InputValue value)
    {
        if(type!=Type.Player) return;

        Vector2 input = value.Get<Vector2>();

        EventManager.Current.OnMoveX(gameObject, input.x);
        EventManager.Current.OnMoveY(gameObject, input.y);
    }

    void OnJump(InputValue value)
    {
        if(type!=Type.Player) return;

        float input = value.Get<float>();

        EventManager.Current.OnJump(gameObject, input);
    }

    void OnSwitch()
    {
        if(type!=Type.Player) return;

        EventManager.Current.OnTrySwitch(gameObject);
    }

    // ============================================================================

    void SwitchPilot(GameObject from, GameObject to)
    {
        if(gameObject==from)
        {
            type = Type.AI;
        }
        else if(gameObject==to)
        {
            type = Type.Player;
        }
    }
}
