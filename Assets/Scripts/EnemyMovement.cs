using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Road> _roads = new List<Road>();
    [SerializeField] [Range(1.0f, 5.0f)] float enemySpeed = 1.0f;

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
                time += Time.deltaTime * enemySpeed;
                transform.position = Vector3.Lerp(startPos, endPos, time);

                yield return new WaitForEndOfFrame();
            }
        }
    }

}
