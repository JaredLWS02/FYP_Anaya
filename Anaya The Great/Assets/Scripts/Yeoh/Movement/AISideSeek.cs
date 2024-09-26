using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SideMove))]

public class AISideSeek : MonoBehaviour
{
    SideMove move;

    void Awake()
    {
        move = GetComponent<SideMove>();
    }

    public void Move()
    {
        move.inputX = GetMoveDir();
    }

    // ============================================================================

    public Transform target;
    
    [HideInInspector]
    public bool arrival;

    [Header("Arrival")]
    public float stoppingRange=.05f;
    public float slowingRangeOffset=.5f;

    // ============================================================================

    float GetMoveDir()
    {
        if(!target) return 0;
        return GetSeekDir();
    }

    float GetSeekDir()
    {
        float max_speed = 1;
        float speed;

        if(arrival)
        {
            float distance = Mathf.Abs(target.position.x - transform.position.x);

            if(distance <= stoppingRange)
            {
                speed=0;
            }
            else
            {
                float ramped = max_speed * distance / (stoppingRange+slowingRangeOffset);

                float clipped = Mathf.Min(ramped, max_speed);

                speed = clipped;
            }
        }
        else speed = max_speed;

        return target.position.x >= transform.position.x ? speed : -speed;
    }
}
