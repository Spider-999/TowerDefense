using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    // Matches the Unity editor settings for the grid size
    [SerializeField] private int _unityGridSize = 10;
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10,10);
    private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

    #region Properties
    public Dictionary<Vector2Int, Node> Grid
    {
        get => _grid;
    }

    public int UnityGridSize
    {
        get => _unityGridSize;
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

    public void SetNonPlaceableNode(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates))
        {
            // Debug.Log("Setting non placeable node at: " + coordinates);
            _grid[coordinates].IsPlaceable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in _grid)
        {
            entry.Value.IsExplored = false;
            entry.Value.IsRoad = false;
            entry.Value.NextNode = null;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / _unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / _unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * _unityGridSize;
        position.y = 0.0f;
        position.z = coordinates.y * _unityGridSize;
        return position;
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
