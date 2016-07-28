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

        inspector.start_row = EditorGUILayout.DelayedIntField("Start Row", inspector.start_row);
        inspector.x_column = EditorGUILayout.DelayedIntField("X Column", inspector.x_column);
        inspector.y_column = EditorGUILayout.DelayedIntField("Y Column", inspector.y_column);
        inspector.Vx_column = EditorGUILayout.DelayedIntField("Vx Column", inspector.Vx_column);
        inspector.Vy_column = EditorGUILayout.DelayedIntField("Vy Column", inspector.Vy_column);
        inspector.Vz_column = EditorGUILayout.DelayedIntField("Vz Column", inspector.Vz_column);
        inspector.V_column = EditorGUILayout.DelayedIntField("V Column", inspector.V_column);
        inspector.t_column = EditorGUILayout.DelayedIntField("t Column", inspector.t_column);
        inspector.PMV_column = EditorGUILayout.DelayedIntField("PMV Column", inspector.PMV_column);

        inspector.gridsize = EditorGUILayout.DelayedFloatField("Grid Size", inspector.gridsize);

        inspector.enableAdvancedFilter = EditorGUILayout.Toggle("Show Data Filtering Options", inspector.enableAdvancedFilter);
        if (inspector.enableAdvancedFilter)
        {
            EditorGUILayout.LabelField("Boundary Start");
            inspector.boundaryStartX = EditorGUILayout.DelayedFloatField("X", inspector.boundaryStartX);
            inspector.boundaryStartY = EditorGUILayout.DelayedFloatField("Y", inspector.boundaryStartY);
            EditorGUILayout.LabelField("Boundary End");
            inspector.boundaryEndX = EditorGUILayout.DelayedFloatField("X", inspector.boundaryEndX);
            inspector.boundaryEndY = EditorGUILayout.DelayedFloatField("Y", inspector.boundaryEndY);
            EditorGUILayout.LabelField("");
            inspector.maxpoints = EditorGUILayout.DelayedIntField("Max CFD Points", inspector.maxpoints);
            inspector.maxVelocityFilter = EditorGUILayout.DelayedFloatField("Max Velocity Filter", inspector.maxVelocityFilter);
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
