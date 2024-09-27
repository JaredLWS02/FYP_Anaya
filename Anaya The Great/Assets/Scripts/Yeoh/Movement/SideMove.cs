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
    public float dirX;

    void Move()
    {
        if(!canMove) dirX=0;

        dirX = vehicle.Round(dirX, 1);
        dirX = Mathf.Clamp(dirX, -1, 1);

        vehicle.Move(vehicle.maxSpeed * dirX, Vector2.right);

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
            if((dirX>0 && faceR) || (dirX<0 && !faceR))
            {
                Flip();
            }
        }
        else
        {
            if((dirX<0 && faceR) || (dirX>0 && !faceR))
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
