using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    #region Private attributes
    [SerializeField] private Vector2Int _startCoordinates;
    [SerializeField] private Vector2Int _endCoordinates;
    private Node _startNode;
    private Node _endNode;
    private Node _currentSearchNode;

    private Dictionary<Vector2Int, Node> _explored = new Dictionary<Vector2Int, Node>();
    private Queue<Node> _frontNodes = new Queue<Node>();

    private Vector2Int[] _directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    private GridManager _gridManager;
    private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();
    #endregion

    #region Properties
    public Vector2Int StartCoordinates
    {
        get => _startCoordinates;
    }

    public Vector2Int EndCoordinates
    {
        get => _endCoordinates;
    }
    #endregion

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();

        if (_gridManager != null)
        {
            _grid = _gridManager.Grid;
            // Get the start and end nodes from the grid dictionary in the 
            // grid manager using the start and end coordinates
            _startNode = _grid[_startCoordinates];
            _endNode = _grid[_endCoordinates];
        }
    }

    private void Start()
    {
        GetNewRoad();
    }

    public List<Node> GetNewRoad()
    {
        _gridManager.ResetNodes();
        BFS();
        return BuildRoad();
    }

    private void ExploreNeighborNodes()
    {
        List<Node> neighborNodes = new List<Node>();

        foreach (Vector2Int direction in _directions)
        {
            Vector2Int neighborNodeCoordinates = _currentSearchNode.GridPosition + direction;
            // Search if there is a neighbor node in the grid
            if (_grid.ContainsKey(neighborNodeCoordinates))
            {
                // Add the neighbor node to the list
                neighborNodes.Add(_grid[neighborNodeCoordinates]);

            }
        }

        foreach (Node neighborNode in neighborNodes)
        {
            // Check if the neighbor node has not been explored and is placeable
            if (!_explored.ContainsKey(neighborNode.GridPosition) && neighborNode.IsPlaceable)
            {
                neighborNode.NextNode = _currentSearchNode;
                // Add the next node to the neighbor node
                // to create a tree structure
                _explored.Add(neighborNode.GridPosition, neighborNode);
                // Add the neighbor node to the queue
                _frontNodes.Enqueue(neighborNode);
            }
        }
    }

    // Breadth First Search algortihm
    private bool BFS()
    {
        _startNode.IsPlaceable = true;
        _endNode.IsPlaceable = true;

        _frontNodes.Clear();
        _explored.Clear();

        _frontNodes.Enqueue(_startNode);
        _explored.Add(_startCoordinates, _startNode);

        while (_frontNodes.Count > 0)
        {
            _currentSearchNode = _frontNodes.Dequeue();
            _currentSearchNode.IsExplored = true;
            ExploreNeighborNodes();

            if (_currentSearchNode.GridPosition == _endCoordinates)
            {
                Debug.Log("Road Found");
                return true;
            }
        }

        return false;
    }

    private List<Node> BuildRoad()
    {
        List<Node> road = new List<Node>();
        Node currentNode = _endNode;

        // Add the end node to the road list
        road.Add(currentNode);
        currentNode.IsRoad = true;

        // Build the road from the end node to the start node
        while (currentNode.NextNode != null)
        {
            currentNode = currentNode.NextNode;
            road.Add(currentNode);
            currentNode.IsRoad = true;
        }

        // Reverse the road list to get the correct order
        road.Reverse();
        return road;
    }

    public bool WillBlockRoad(Vector2Int coordinates)
    {
        if(_grid.ContainsKey(coordinates))
        {
            bool previousState = _grid[coordinates].IsPlaceable;
            _grid[coordinates].IsPlaceable = false;
            List<Node> newRoad = GetNewRoad();
            _grid[coordinates].IsPlaceable = previousState;

            if(newRoad.Count <= 1)
            {
                GetNewRoad();
                return true;
            }
        }

        return false;
    }
}
