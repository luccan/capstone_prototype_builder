using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NoiseSelect))]
public class NoisePresets : Editor
{
    string[] _choices = new[] { "City", "Neighbourhood", "Seaside", "Television", "From Enclosed Room" };
    int _choiceIndex = 0;

    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
        var a = target as NoiseSelect;
        // Update the selected choice in the underlying object
        a.SelectPreset = _choices[_choiceIndex];
        // Save the changes back to the object
        EditorUtility.SetDirty(target);
    }

}
