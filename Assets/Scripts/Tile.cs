using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tower _cannonPrefab;
    [SerializeField] private float _cannonSpawnY;
    [SerializeField] private bool _isPlaceable;
    private GridManager _gridManager;
    private PathFinding _pathFinding;
    private Vector2Int _coordinates = new Vector2Int();

    #region Properties
    public bool IsPlaceable
    {
        get => _isPlaceable;
        set => _isPlaceable = value;
    }
    #endregion

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        _pathFinding = FindObjectOfType<PathFinding>();
    }

    private void Start()
    {
        if(_gridManager != null)
        {
            _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);
            if (!_isPlaceable)
                _gridManager.SetNonPlaceableNode(_coordinates);
        }
    }

    private void OnMouseDown()
    {
        if (_gridManager.GetNode(_coordinates).IsPlaceable && !_pathFinding.WillBlockRoad(_coordinates))
        {
            Vector3 cannonSpawnLocation = new Vector3(transform.position.x, _cannonSpawnY, transform.position.z);
            bool isPlaced;

            // Place a cannon tower to the specified spawn location
            // if the player has enough currency to place the tower
            isPlaced = _cannonPrefab.PlaceTower(_cannonPrefab, cannonSpawnLocation);

            // Make the tile non placeble if it is occupied already
            // and if the player had enough currency to place the tower
            IsPlaceable = !isPlaced;
            _gridManager.SetNonPlaceableNode(_coordinates);
            // Log the tile location
            Debug.Log(transform.name);
        }
    }
}
