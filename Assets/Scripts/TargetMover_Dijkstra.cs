using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component moves its object towards a given target position.
 */
public class TargetMover_Dijkstra : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;

    [SerializeField] int weight = 1;

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;

    protected bool atTarget;  // This property is set to "true" whenever the object has already found the target.

    public void SetTarget(Vector3 newTarget)
    {
        if (targetInWorld != newTarget)
        {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
        }
    }

    public Vector3 GetTarget()
    {
        return targetInWorld;
    }

    private TileMapDijkstra tilemapDijkstra = null;
    private float timeBetweenSteps;


    protected virtual void Start()
    {
        tilemapDijkstra = new TileMapDijkstra(tilemap, allowedTiles.Get(), weight);
        timeBetweenSteps = 1 / speed;
        StartCoroutine(MoveTowardsTheTarget());
    }

    IEnumerator MoveTowardsTheTarget()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget()
    {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        Vector3Int endNode = targetInGrid;
        List<Vector3Int> shortestPath = Dijkstra.GetPath(tilemapDijkstra, startNode, endNode);
        Debug.Log("shortestPath = " + string.Join(" , ", shortestPath));
        if (shortestPath.Count >= 2)
        {
            Vector3Int nextNode = shortestPath[1];
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        }
        else
        {
            atTarget = true;
        }
    }
}
