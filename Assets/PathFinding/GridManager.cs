using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize = new Vector2Int(10,10);
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);
                // Add a new node to the grid with default values
                grid.Add(gridPosition, new Node(gridPosition, true));
            }
        }
    }
}
