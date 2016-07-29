using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(OBJImports))]
public class OBJBuilder : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        OBJImports myScript = (OBJImports)target;
        if (GUILayout.Button("Build Objects"))
        {
            myScript.BuildWalls();
        }
    }
}
