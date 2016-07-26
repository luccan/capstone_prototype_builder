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
            GenerateHumanFigurineCsv(path);
            GenerateHumanLocationCsv(path);
            GenerateCFDCsv(path);
            ExportAssetBundles.ExportResource(1, path);
        }
    }

    static void WriteFile(string path, string filename, string content)
    {
        if (path.Length != 0)
        {
            System.IO.File.WriteAllText(path + @"/" + filename, content);
        }
    }

    static void GenerateHumanFigurineCsv(string path)
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

    static void GenerateHumanLocationCsv(string path)
    {
        List<Vector3> hloccoords = new List<Vector3>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("HumanLocation"))
        {
            Vector3 pos = obj.transform.position;
            hloccoords.Add(new Vector3(pos.x, pos.y, pos.z));
        }
        string csv = "";
        foreach (Vector3 cor in hloccoords)
        {
            csv += string.Format("{0},{1},{2}\n", cor.x, cor.y, cor.z);
        }

        WriteFile(path, "jumplocations.csv", csv);
    }

    static void GenerateCFDCsv(string path)
    {
        CSVImports target = GameObject.Find("CSVImporter").GetComponent<CSVImports>();

        WriteFile(path, "cfd.csv", target.csv);
    }

    static void GenerateCFDOriginCsv(string path)
    {
        Vector3 cor = GameObject.Find("CFDOrigin").transform.position;

        string csv = string.Format("{0},{1},{2}\n", cor.x, cor.y, cor.z);

        WriteFile(path, "cfdorigin.csv", csv);
    }

    static void GenerateLayerSettingsCsv(string path)
    {
        LayerSettings target = GameObject.Find("Settings").GetComponent<LayerSettings>();

        WriteFile(path, "layersettings.csv", target.csv);
    }

}
