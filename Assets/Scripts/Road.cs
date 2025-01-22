using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] GameObject cannonPrefab;
    [SerializeField] bool isPlaceable;
    [SerializeField] float cannonSpawnY;

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            var cannonSpawnLocation = new Vector3(transform.position.x, cannonSpawnY, transform.position.z);
            // Instantiate a cannon tower to the specified spawn location
            Instantiate(cannonPrefab,cannonSpawnLocation,Quaternion.identity);
            // Make the tile non placeble if it is occupied already
            isPlaceable = false;
            // Log the tile location
            Debug.Log(transform.name);
        }
    }
}
