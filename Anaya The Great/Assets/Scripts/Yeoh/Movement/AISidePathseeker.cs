using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(AISideSeek))]
[RequireComponent(typeof(Jump2D))]

public class AISidePathseeker : MonoBehaviour
{
    Seeker seeker;
    AISideSeek aiMove;
    Jump2D jump;

    void Awake()
    {
        seeker = GetComponent<Seeker>();
        aiMove = GetComponent<AISideSeek>();
        jump = GetComponent<Jump2D>();
    }

    // ============================================================================
    
    Path path=null;

    [Header("Path")]
    public Transform target;
    public Vector3 targetOffset = new Vector3(0, .5f, 0);
    public Vector3 selfOffset = new Vector3(0, .5f, 0);

    // Update Path ============================================================================

    public float UpdatePathInterval=.25f;

    void OnEnable()
    {
        StartCoroutine(MakingPath());
    }

    IEnumerator MakingPath()
    {
        while(true)
        {
            yield return new WaitForSeconds(UpdatePathInterval);
            TryMakePath(transform.position + selfOffset, target.position + targetOffset);
        }
    }

    void TryMakePath(Vector3 from, Vector3 to)
    {
        if(!seeker.IsDone()) return;

        seeker.StartPath(from, to, OnPathCreated);
    }

    void OnPathCreated(Path new_path)
    {
        if(new_path.error) return;

        List<Vector3> new_nodes = new(new_path.vectorPath);

        // remove all the nodes that are within stopping range
        foreach(Vector3 node in new_path.vectorPath)
        {
            float distance = Vector3.Distance(node, target.position + targetOffset);

            if(distance <= aiMove.stoppingRange)
            {
                new_nodes.Remove(node);
            }
        }

        new_path.vectorPath = new_nodes;

        path = new_path;
        currentNode = Mathf.Min(startingNode, path.vectorPath.Count-1);
    }

    // Moving ============================================================================

    [Header("Move")]
    public int startingNode=2;
    public float nextNodeRange=1;
    int currentNode;

    public void Move()
    {
        if(path==null) return;
        if(path.vectorPath.Count==0) return;

        Vector3 targetNode = path.vectorPath[currentNode];
        TryContinue(targetNode);

        aiMove.arrival = HasReachedEnd();
        aiMove.targetPos = targetNode;
        aiMove.Move();

        TryJump(targetNode);
    }

    void TryContinue(Vector3 targetNode)
    {
        float distance = Vector3.Distance(transform.position + selfOffset, targetNode);

        if(distance <= nextNodeRange)
        {
            if(HasReachedEnd())
                path=null;
            else
                currentNode++;
        }
    }

    bool HasReachedEnd()
    {
        if(path==null) return true;

        return currentNode >= path.vectorPath.Count-1;
    }

    // Jumping ============================================================================

    [Header("Jump")]
    public float jumpIfAboveHeight=1.5f;
    public float jumpCutIfBelowHeight=.5f;

    void TryJump(Vector3 targetNode)
    {
        float y_dist = GetYDistance(targetNode);

        if(IsTargetAbove(targetNode))
        {
            if(y_dist >= jumpIfAboveHeight)
            {
                jump.JumpBuffer();
            }
        }
        else
        {
            if(y_dist >= jumpCutIfBelowHeight)
            {
                jump.JumpCut();
            }
        }
    }

    float GetYDistance(Vector3 target)
    {
        return Mathf.Abs(transform.position.y - target.y);
    }

    bool IsTargetAbove(Vector3 target)
    {
        return target.y >= transform.position.y;
    }
}
