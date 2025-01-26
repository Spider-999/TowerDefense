using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Road> _roads = new List<Road>();
    [SerializeField] [Range(1.0f, 5.0f)] private float _enemySpeed = 1.0f;

    // This method is called when the script instance is being loaded.
    private void Awake()
    {
        /* 
         * Instead of building the path in the OnEnable method
         * every time the object is enabled, we can build the path
         * only once in the Awake method for more efficient resource usage.
        */
        FindAndBuildPath();
    }

    // This method is called when the object becomes enabled and active.
    private void OnEnable()
    {
        ReturnToBeginning();
        StartCoroutine(FollowRoad());
    }

    private void FindAndBuildPath()
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

    private void ReturnToBeginning()
    {
        // Get the first road from the road list.
        Vector3 firstRoad = _roads.First().transform.position;

        // Place the enemy at the beginning of the road.
        // Keep the y position of the enemy the same.
        transform.position = new Vector3(firstRoad.x, transform.position.y, firstRoad.z);
    }

    // Coroutine
    private IEnumerator FollowRoad()
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
        // Instead of destroying the enemy, we can just disable it
        // and set it dormant in the object pool until it is needed again.
        gameObject.SetActive(false);
    }

}
