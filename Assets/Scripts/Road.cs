using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private GameObject _cannonPrefab;
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
            var cannonSpawnLocation = new Vector3(transform.position.x, _cannonSpawnY, transform.position.z);
            // Instantiate a cannon tower to the specified spawn location
            Instantiate(_cannonPrefab,cannonSpawnLocation,Quaternion.identity);
            // Make the tile non placeble if it is occupied already
            IsPlaceable = false;
            // Log the tile location
            Debug.Log(transform.name);
        }
    }
}
