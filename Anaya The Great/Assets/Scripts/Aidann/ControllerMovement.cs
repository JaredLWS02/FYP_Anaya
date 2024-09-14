using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private Vector2 moveInputValue;
    private float jumpForce = 10f;
    private bool isJumping;

    private void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
        Debug.Log(moveInputValue);
    }

    private void OnJump()
    {
        if (IsGrounded())
        {
            Debug.Log("Jump pressed");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void MoveLogicMethod()
    {
        Vector2 horizontalMovement = new Vector2(moveInputValue.x, 0);

        Vector2 result = horizontalMovement * speed * Time.fixedDeltaTime;

        rb.velocity = new Vector2(result.x, rb.velocity.y);
    }

    private void FixedUpdate()
    {
        MoveLogicMethod();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
