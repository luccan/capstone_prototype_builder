using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class GenerateCsvFiles {

    [MenuItem("SKYOpt/Generate SkyBundle")]
    static void GenerateSkyBundle()
    {
        string path = EditorUtility.SaveFilePanel("Generate SkyBundle", "", "MySkyBundle", "sky");
        if (path.Length != 0)
        {
            System.IO.Directory.CreateDirectory(path);
            GenerateHumanCsv(path);
            GenerateCFDCsv(path);
        }
    }

    static void WriteFile(string path, string filename, string content)
    {
        if (path.Length != 0)
        {
            System.IO.File.WriteAllText(path + @"/" + filename, content);
        }
    }

    static void GenerateHumanCsv(string path)
    {
        List<Vector4> humancoords = new List<Vector4>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("HumanFigurine"))
        {
            //Object parentObject = PrefabUtility.GetPrefabParent(obj);
            //string path = AssetDatabase.GetAssetPath(parentObject);
            Vector3 pos = obj.transform.position;
            humancoords.Add(new Vector4(pos.x, pos.y, pos.z, (float) obj.transform.rotation.eulerAngles.y));
        }
        string csv = "";
        foreach (Vector4 cor in humancoords)
        {
            csv += string.Format("{0},{1},{2},{3}\n", cor.x, cor.y, cor.z, cor.w);
        }

        WriteFile(path, "humancoords.csv", csv);
    }

    static void GenerateCFDCsv(string path)
    {
        CSVImports target = GameObject.Find("CSVImporter").GetComponent<CSVImports>();

        WriteFile(path, "cfd.csv", target.csv);
    }

}
