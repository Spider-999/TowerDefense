using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The Node class represents a single node in the grid.
/// The grid manager holds all of the nodes.
/// The class is serializable so that it can be viewed in the inspector.
/// </summary>
public class Node
{
    #region Private attributes
    public Vector2Int GridPosition;
    public bool IsWalkable;
    public bool IsExplored;
    public bool IsRoad;
    public Node NextNode;
    #endregion

    public Node(Vector2Int gridPosition, bool isWalkable)
    {
        GridPosition = gridPosition;
        IsWalkable = isWalkable;
    }
}
