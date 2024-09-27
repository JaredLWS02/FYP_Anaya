using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(AISideSeek))]

public class AISidePathseeker : MonoBehaviour
{
    Seeker seeker;
    AISideSeek aiMove;

    void Awake()
    {
        seeker = GetComponent<Seeker>();
        aiMove = GetComponent<AISideSeek>();
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

        new_path.vectorPath = GetNodesOutsideStoppingRange(new_path.vectorPath);

        if(new_path.vectorPath.Count==0) return;

        path = new_path;
        currentNodeIndex = Mathf.Min(startingNodeIndex, path.vectorPath.Count-1);
    }

    List<Vector3> GetNodesOutsideStoppingRange(List<Vector3> path_nodes)
    {
        List<Vector3> new_nodes = new();

        // add all the nodes that are outside stopping range
        foreach(Vector3 node in path_nodes)
        {
            float distance = Vector3.Distance(node, target.position + targetOffset);

            if(distance >= aiMove.stoppingRange)
            {
                new_nodes.Add(node);
            }
        }

        return new_nodes;
    }

    // Moving ============================================================================

    [Header("Move")]
    public int startingNodeIndex=2;
    int currentNodeIndex;
    //public float nextNodeRange=1;

    public void Move()
    {
        if(path==null) return;
        if(path.vectorPath.Count==0) return;

        Vector3 targetNode = path.vectorPath[currentNodeIndex];
        TryContinue(targetNode);

        aiMove.arrival = HasReachedEnd();
        aiMove.targetPos = targetNode;
        aiMove.Move();

        CheckNodeHeight(targetNode);
    }

    void TryContinue(Vector3 targetNode)
    {
        float distance = Vector3.Distance(transform.position + selfOffset, targetNode);

        float nextNodeRange = aiMove.stoppingRange;

        if(distance <= nextNodeRange)
        {
            if(HasReachedEnd())
            {
                path=null;
            }
            else
            {
                currentNodeIndex++;
            }
        }
    }

    bool HasReachedEnd()
    {
        if(path==null) return true;

        return currentNodeIndex >= path.vectorPath.Count-1;
    }

    // For Jump or Descend ============================================================================

    void CheckNodeHeight(Vector3 targetNode)
    {
        Vector3 selfPos = transform.position + selfOffset;

        float node_height = targetNode.y - selfPos.y;

        float nextNodeRange = aiMove.stoppingRange;

        // node is above
        if(node_height > nextNodeRange)
        {
            EventManager.Current.OnJump(gameObject, 1);
        }
        // node is below
        else if(node_height < -nextNodeRange)
        {
            EventManager.Current.OnJump(gameObject, 0); // jumpcut
            EventManager.Current.OnMoveY(gameObject, -1); // press down
        }
    }
}
