using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(CSVImports))]
public class CSVImportsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        CSVImports inspector = (CSVImports)target; //set the target inspector window
        inspector.CFD = (TextAsset)EditorGUILayout.ObjectField("CFD File", inspector.CFD, typeof(TextAsset), true);

        base.OnInspectorGUI();

        inspector.start_row = EditorGUILayout.IntField("Start Row", inspector.start_row);
        inspector.x_column = EditorGUILayout.IntField("X Column", inspector.x_column);
        inspector.y_column = EditorGUILayout.IntField("Y Column", inspector.y_column);
        inspector.Vx_column = EditorGUILayout.IntField("Vx Column", inspector.Vx_column);
        inspector.Vy_column = EditorGUILayout.IntField("Vy Column", inspector.Vy_column);
        inspector.Vz_column = EditorGUILayout.IntField("Vz Column", inspector.Vz_column);
        inspector.V_column = EditorGUILayout.IntField("V Column", inspector.V_column);
        inspector.t_column = EditorGUILayout.IntField("t Column", inspector.t_column);
        inspector.PMV_column = EditorGUILayout.IntField("PMV Column", inspector.PMV_column);

        inspector.gridsize = EditorGUILayout.FloatField("Grid Size", inspector.gridsize);

        inspector.enableAdvancedFilter = EditorGUILayout.Toggle("Show Data Filtering Options", inspector.enableAdvancedFilter);
        if (inspector.enableAdvancedFilter)
        {
            inspector.boundaryStart = EditorGUILayout.Vector2Field("Boundary Start", inspector.boundaryStart);
            inspector.boundaryEnd = EditorGUILayout.Vector2Field("Boundary End", inspector.boundaryEnd);
            inspector.maxpoints = EditorGUILayout.IntField("Max CFD Points", inspector.maxpoints);
            inspector.maxVelocityFilter = EditorGUILayout.FloatField("Max Velocity Filter", inspector.maxVelocityFilter);
        }

        if (inspector.csvhead.Count > 0)
        {
            //display table
            float win = Screen.width * 0.7f;
            float colwidth = win / inspector.csvhead[0].Count;

            GUILayout.Label("HEAD of Parsed Data");
            foreach (List<string> row in inspector.csvhead)
            {
                GUILayout.BeginHorizontal();
                foreach (string data in row)
                {
                    GUILayout.Label(data, GUILayout.Width(colwidth));
                }
                GUILayout.EndHorizontal();
            }
        }
        //end of file display
    }
}
