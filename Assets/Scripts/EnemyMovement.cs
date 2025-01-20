using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Road> _roads = new List<Road>();
    [SerializeField] float waitTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowRoad());
    }

    // Coroutine
    IEnumerator FollowRoad()
    {
        foreach (Road road in _roads)
        {
            // Set the position of the enemy to the position of
            // the current road in the foreach loop
            transform.position = road.transform.position;

            // Go back to Start for one sec, then come back in the
            // loop and print the next road name
            yield return new WaitForSeconds(waitTime);
        }
    }

}
