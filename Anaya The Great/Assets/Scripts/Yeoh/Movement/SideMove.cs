using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ForceVehicle2D))]

public class SideMove : MonoBehaviour
{
    ForceVehicle2D vehicle;

    void Awake()
    {
        vehicle = GetComponent<ForceVehicle2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    // ============================================================================

    public bool canMove=true;
    public float inputX;

    void Move()
    {
        if(!canMove) return;

        inputX = vehicle.Round(inputX, 1);
        inputX = Mathf.Clamp(inputX, -1, 1);

        vehicle.Move(vehicle.maxSpeed * inputX, Vector2.right);

        TryFlip();
    }

    // ============================================================================

    [Header("Flip")]
    public bool faceR=true;
    public bool reverse;

    void TryFlip()
    {
        if(reverse)
        {
            if((inputX>0 && faceR) || (inputX<0 && !faceR))
            {
                Flip();
            }
        }
        else
        {
            if((inputX<0 && faceR) || (inputX>0 && !faceR))
            {
                Flip();
            }
        }
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        faceR=!faceR;
    }
}
