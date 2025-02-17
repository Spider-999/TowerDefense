using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 5.0f)] private float _enemySpeed = 1.0f;
    private List<Node> _roads = new List<Node>();
    private Enemy _enemy;
    private GridManager _gridManager;
    private PathFinding _pathFinding;

    // This method is called when the script instance is being loaded.
    private void OnEnable()
    {
        FindAndBuildPath();
        ReturnToBeginning();
        StartCoroutine(FollowRoad());
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _gridManager = FindObjectOfType<GridManager>();
        _pathFinding = FindObjectOfType<PathFinding>();
    }

    private void FindAndBuildPath()
    {
        // Clear the existing roads
        _roads.Clear();
        _roads = _pathFinding.GetNewRoad();
    }

    private void ReturnToBeginning()
    {
        // Get the first road from the gridmanager
        Vector3 firstRoad = _gridManager.GetPositionFromCoordinates(_pathFinding.StartCoordinates);
        
        // Place the enemy at the beginning of the road.
        // Keep the y position of the enemy the same.
        transform.position = new Vector3(firstRoad.x, transform.position.y, firstRoad.z);
    }

    void FinishRoadFollow()
    {
        // Instead of destroying the enemy, we can just disable it
        // and set it dormant in the object pool until it is needed again.
        gameObject.SetActive(false);

        // The player loses money when the enemy reaches the end
        _enemy.LoseCurrency();
    }

    // Coroutine
    private IEnumerator FollowRoad()
    {
        for(int i = 0; i < _roads.Count; i++)
        {
            // Enemy position
            Vector3 startPos = transform.position;
            // Give the end position the start position's y
            // because we dont want to interpolate on the y axis.
            Vector3 endPos = _gridManager.GetPositionFromCoordinates(_roads[i].GridPosition);
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

        FinishRoadFollow();
    }

}
