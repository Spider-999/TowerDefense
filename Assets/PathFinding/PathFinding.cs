using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
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

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();

        if (_gridManager != null)
            _grid = _gridManager.Grid;

        // The first node and end node are walkable
        _startNode = new Node(_startCoordinates, true);
        _endNode = new Node(_endCoordinates, true);
    }

    void Start()
    {
        BFS();
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

        foreach(Node neighborNode in neighborNodes)
        {
            // Check if the neighbor node has not been explored and is walkable
            if (!_explored.ContainsKey(neighborNode.GridPosition) && neighborNode.IsWalkable)
            {
                // Add the neighbor node to the queue
                _frontNodes.Enqueue(neighborNode);
                _explored.Add(neighborNode.GridPosition, neighborNode);
            }
        }
    }

    // Breadth First Search algortihm
    private void BFS()
    {
        _frontNodes.Enqueue(_startNode);
        _explored.Add(_startCoordinates, _startNode);

        while (_frontNodes.Count > 0)
        {
            _currentSearchNode = _frontNodes.Dequeue();
            _currentSearchNode.IsExplored = true;
            ExploreNeighborNodes();

            if(_currentSearchNode.GridPosition == _endCoordinates)
            {
                Debug.Log("Road Found");
                break;
            }   
        }
    }
}
