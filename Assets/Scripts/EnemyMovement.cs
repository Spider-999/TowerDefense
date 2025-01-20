using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Road> _roads = new List<Road>();

    // Start is called before the first frame update
    void Start()
    {
        PrintRoads();
    }

    void PrintRoads()
    {
        foreach (Road road in _roads)
            Debug.Log(road.name);
    }

}
