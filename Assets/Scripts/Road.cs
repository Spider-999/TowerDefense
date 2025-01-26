using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Tower _cannonPrefab;
    [SerializeField] private float _cannonSpawnY;
    [SerializeField] private bool _isPlaceable = true;

    #region Properties
    public bool IsPlaceable
    {
        get => _isPlaceable;
        set => _isPlaceable = value;
    }
    #endregion

    private void OnMouseDown()
    {
        if (IsPlaceable)
        {
            Vector3 cannonSpawnLocation = new Vector3(transform.position.x, _cannonSpawnY, transform.position.z);
            bool isPlaced;

            // Place a cannon tower to the specified spawn location
            // if the player has enough currency to place the tower
            isPlaced = _cannonPrefab.PlaceTower(_cannonPrefab, cannonSpawnLocation);

            // Make the tile non placeble if it is occupied already
            // and if the player had enough currency to place the tower
            IsPlaceable = !isPlaced;

            // Log the tile location
            Debug.Log(transform.name);
        }
    }
}
