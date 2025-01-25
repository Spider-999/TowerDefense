using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Road> _roads = new List<Road>();
    [SerializeField] [Range(1.0f, 5.0f)] private float _enemySpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        FindAndBuildPath();
        StartCoroutine(FollowRoad());
    }

    void FindAndBuildPath()
    {
        // Clear the existing roads
        _roads.Clear();

        // Get the parent of object of the roads with the tag "Road"
        GameObject parentRoad = GameObject.FindGameObjectWithTag("Road");

        // Loop through all the children of the parentRoad
        // and add them to the _roads list to build the level's road.
        foreach (Transform road in parentRoad.transform)
            _roads.Add(road.GetComponent<Road>());
    }

        // Coroutine
        IEnumerator FollowRoad()
    {
        foreach (Road road in _roads)
        {
            // Enemy position
            Vector3 startPos = transform.position;
            // Give the end position the start position's y
            // because we dont want to interpolate on the y axis.
            Vector3 endPos = new Vector3(road.transform.position.x, 
                                         startPos.y, 
                                         road.transform.position.z);
            float time = 0.0f;



            // Linear interpolate the enemy position to smoothen
            // movement between tiles.
            while (time < 1.0f)
            {
                // Every frame we update the time to travel between
                // the start position and end position
                time += Time.deltaTime * _enemySpeed;
                transform.position = Vector3.Lerp(startPos, endPos, time);

                yield return new WaitForEndOfFrame();
            }
        }
    }

}
