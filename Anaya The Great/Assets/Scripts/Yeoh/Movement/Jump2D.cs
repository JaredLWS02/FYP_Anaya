using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Jump2D : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateExtraJumps();
        UpdateJumpBuffer();
        UpdateCoyoteTime();
        
        TryJump();
    }

    // Jump ============================================================================
    
    public bool canJump=true;
    public float jumpForce=8;

    void TryJump()
    {
        if(!canJump) return;

        if(!HasJumpBuffer()) return;

        if(HasCoyoteTime())
        {
            Jump();            
        }
        else if(extraJumpsLeft>0)
        {
            extraJumpsLeft--;
            Jump();
        }
    }

    void Jump()
    {
        if(isJumpCooling) return;
        StartCoroutine(JumpCooling());

        rb.velocity = new Vector2(rb.velocity.x, 0);

        rb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);

        jumpBufferLeft = 0;
        coyoteTimeLeft = 0;
    }

    // Extra Jump ============================================================================

    [Header("Extra Jumps")]
    public int extraJumps=1;
    int extraJumpsLeft;
    public float jumpCooldown=.1f;
    bool isJumpCooling;

    void UpdateExtraJumps()
    {
        if(IsGrounded())
        {
            extraJumpsLeft = extraJumps;
        }
    }

    IEnumerator JumpCooling()
    {
        isJumpCooling=true;
        yield return new WaitForSeconds(jumpCooldown);
        isJumpCooling=false;
    }

    // Jump Buffer ============================================================================

    [Header("Jump Buffer")]
    public float jumpBufferTime=.2f;
    float jumpBufferLeft;

    public void JumpBuffer()
    {
        jumpBufferLeft = jumpBufferTime;
    }

    void UpdateJumpBuffer()
    {
        jumpBufferLeft -= Time.deltaTime;
    }

    bool HasJumpBuffer()
    {
        return jumpBufferLeft>0;
    }

    // Coyote Time ============================================================================

    [Header("Coyote Time")]
    public float coyoteTime=.2f;
    float coyoteTimeLeft;

    void UpdateCoyoteTime()
    {
        coyoteTimeLeft -= Time.deltaTime;

        if(IsGrounded())
        {
            coyoteTimeLeft = coyoteTime;
        }
    }

    bool HasCoyoteTime()
    {
        return coyoteTimeLeft>0;
    }

    // Jump Cut ============================================================================

    [Header("Jump Cut")]
    public float jumpCutMult=.5f;

    public void JumpCut()
    {
        // only if going up
        if(rb.velocity.y>0)
        {
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMult);
            rb.AddForce(Vector2.down * rb.velocity.y * (1-jumpCutMult), ForceMode2D.Impulse);
        }
    }

    // Ground Check ============================================================================

    [Header("Ground Check")]
    public Vector2 boxSize = new Vector2(.2f, .05f);
    public Vector2 boxCenterOffset = Vector2.zero;
    public LayerMask groundLayer;

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox((Vector2)transform.position + boxCenterOffset, boxSize, 0f, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + boxCenterOffset, boxSize);
    }
}
