using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class Pilot : MonoBehaviour
{
    PlayerInput input;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
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

    void Update()
    {
        input.enabled = type==Type.Player;
    }

    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        EventManager.Current.OnMoveX(gameObject, input.x);
        EventManager.Current.OnMoveY(gameObject, input.y);
    }

    void OnJump(InputValue value)
    {
        float input = value.Get<float>();

        EventManager.Current.OnJump(gameObject, input);
    }

    void OnSwitch()
    {
        PlayerManager.Current.TrySwitch(gameObject);
    }
}
