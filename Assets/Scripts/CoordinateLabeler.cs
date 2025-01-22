using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This makes sure the script runs always, even in edit mode.
[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    private TextMeshPro _label;
    private Vector2Int _position = new Vector2Int();
    private Road _road;
    [SerializeField] private Color _canPlaceTower = Color.green;
    [SerializeField] private Color _cantPlaceTower = Color.red;

    // Awake is the first function called in Unity.
    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        // Labels arent shown by default
        _label.enabled = false;

        _road = GetComponentInParent<Road>();
        DisplayCoordinates();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateTileName();
        }

        ToggleLabels();
        ColorCoordinates();
    }

    void DisplayCoordinates()
    {
        _position.x = Mathf.RoundToInt(transform.parent.position.x /
                                       UnityEditor.EditorSnapSettings.move.x);
        _position.y = Mathf.RoundToInt(transform.parent.position.z /
                                       UnityEditor.EditorSnapSettings.move.z);

        _label.text = $"({_position.x},{_position.y})";
    }

    void UpdateTileName()
    {
        transform.parent.name = _position.ToString();
    }

    void ColorCoordinates()
    {
        if(_road.IsPlaceable)
            _label.color = _canPlaceTower;
        else
            _label.color = _cantPlaceTower;
    }

    void ToggleLabels()
    {
        // Toggle the label display when pressing L
        if (Input.GetKeyDown(KeyCode.L))
            _label.enabled = !_label.enabled;
    }
}
