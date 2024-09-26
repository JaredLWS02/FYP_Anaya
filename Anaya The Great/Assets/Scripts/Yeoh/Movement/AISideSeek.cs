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
        move.inputX = GetSeekDir();
    }

    // ============================================================================

    [HideInInspector]
    public Vector3 targetPos;

    [Header("Arrival")]
    public bool arrival=true;
    public float stoppingRange=2;
    public float slowingRangeOffset=2;

    // ============================================================================

    float GetSeekDir()
    {
        float max_speed = 1;
        float speed;

        if(arrival)
        {
            float distance = Mathf.Abs(targetPos.x - transform.position.x);

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

        return targetPos.x >= transform.position.x ? speed : -speed;
    }
}
