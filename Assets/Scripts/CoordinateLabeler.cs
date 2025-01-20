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

    // Awake is the first function called in Unity.
    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
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
}
