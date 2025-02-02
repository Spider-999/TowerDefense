using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10,10);
    private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

    #region Properties
    public Dictionary<Vector2Int, Node> Grid
    {
        get => _grid;
    }

    public Node GetNode(Vector2Int gridPosition)
    {
        // Check if the grid contains the node at the given position
        if (_grid.ContainsKey(gridPosition))
            return _grid[gridPosition];

        return null;
    }
    #endregion

    private void Awake()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for(int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);
                // Add a new node to the grid with default values
                _grid.Add(gridPosition, new Node(gridPosition, true));
            }
        }
    }
}
