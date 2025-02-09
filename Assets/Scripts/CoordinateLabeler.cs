using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This makes sure the script runs always, even in edit mode.
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    #region Private attributes
    private TextMeshPro _label;
    private Vector2Int _position = new Vector2Int();
    [SerializeField] private Color _canPlaceTowerColor = Color.green;
    [SerializeField] private Color _cantPlaceTowerColor = Color.red;
    [SerializeField] private Color _exploredRoadColor = Color.yellow;
    [SerializeField] private Color _roadColor = Color.blue;
    private GridManager _gridManager;
    #endregion

    // Awake is the first function called in Unity.
    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        _label = GetComponent<TextMeshPro>();
        // Labels arent shown by default
        _label.enabled = false;

        DisplayCoordinates();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateTileName();
            _label.enabled = true;
        }

        ToggleLabels();
        ColorCoordinates();
    }

    void DisplayCoordinates()
    {
        if(_gridManager == null)
            return;

        _position.x = Mathf.RoundToInt(transform.parent.position.x / _gridManager.UnityGridSize);
        _position.y = Mathf.RoundToInt(transform.parent.position.z / _gridManager.UnityGridSize);

        _label.text = $"({_position.x},{_position.y})";
    }

    void UpdateTileName()
    {
        transform.parent.name = _position.ToString();
    }

    // Change the color of the coordinate label based on the road's placeable status
    void ColorCoordinates()
    {
        if(_gridManager == null) 
            return;

        Node node = _gridManager.GetNode(_position);

        // Check if a node is found in the dictionary
        if(node == null)
            return;

        if (!node.IsPlaceable)
        {
            _label.color = _cantPlaceTowerColor;
        }
        else if(node.IsRoad)
        {
            _label.color = _roadColor;
        }
        else if(node.IsExplored)
        {
            _label.color = _exploredRoadColor;
        }
        else
        {
            _label.color = _canPlaceTowerColor;
        }
    }

    void ToggleLabels()
    {
        // Toggle the label display when pressing L
        if (Input.GetKeyDown(KeyCode.L))
            _label.enabled = !_label.enabled;
    }
}
