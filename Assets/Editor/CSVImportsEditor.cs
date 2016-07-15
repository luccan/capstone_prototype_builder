using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(CSVImports))]
public class CSVImportsEditor : Editor {

    public override void OnInspectorGUI()
    {
        CSVImports inspector = (CSVImports)target; //set the target inspector window
        inspector.CFD = (TextAsset)EditorGUILayout.ObjectField("CFD File", inspector.CFD, typeof(TextAsset), true);

        base.OnInspectorGUI();

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
