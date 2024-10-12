using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISideWander : MonoBehaviour
{
    public Transform wanderTr;

    // ============================================================================
    
    Vector2 startPos;
    Vector2 targetPos;

    void Awake()
    {
        startPos = transform.position;
        targetPos = transform.position;
    }

    void FixedUpdate()
    {
        wanderTr.position = targetPos;
    }

    // ============================================================================

    public int maxRetries = 1000;
    public Vector2 interval = new(1,4);

    [Header("Ranges")]
    public float innerRadius=1;
    public float outerRadius=5;
    public float maxRangeFromStart=10;

    void OnEnable()
    {
        StartCoroutine(Relocating());
    }

    IEnumerator Relocating()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(interval.x, interval.y));
            Relocate();
        }
    }

    void Relocate()
    {
        if(IsTooFar())
        {
            targetPos = startPos;
            return;
        }

        for(int i=0; i<maxRetries; i++)
        {
            Vector2 random_spot = RandomSpotInDoughnut();

            if(!IsWalkable(random_spot)) continue;
            if(!IsGrounded(random_spot)) continue;

            targetPos = random_spot;
            return;
        }
    }

    // ============================================================================
    
    bool IsTooFar()
    {
        float distanceFromStart = Vector3.Distance(transform.position, startPos);
        return distanceFromStart>maxRangeFromStart;
    }

    bool IsWalkable(Vector2 pos)
    {
        return AstarPath.active.GetNearest(pos).node.Walkable;
    }

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckRange=1;

    bool IsGrounded(Vector2 pos)
    {
        // Raycast down from the position to see if there's ground within a certain distance
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, groundCheckRange, groundLayer);
        return hit.collider != null;
    }

    // ============================================================================

    Vector3 RandomSpotInDoughnut()
    {
        float angle = Random.Range(0, Mathf.PI*2);

        float distance = Mathf.Sqrt(Random.Range(innerRadius*innerRadius, outerRadius*outerRadius));

        float x = distance * Mathf.Cos(angle);
        float y = distance * Mathf.Sin(angle);

        return transform.position + new Vector3(x, y);
    }
    
    // ============================================================================

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 1, .5f);
        Gizmos.DrawWireSphere(transform.position, innerRadius);
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }
}
