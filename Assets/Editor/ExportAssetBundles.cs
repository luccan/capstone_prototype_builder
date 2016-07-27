// Builds an asset bundle from the selected objects in the project view.
// Once compiled go to "Menu" -> "Assets" and select one of the choices
// to build the Asset Bundle
using UnityEngine;
using UnityEditor;
using System.IO;

public class ExportAssetBundles
{
    [MenuItem("SKYOpt/Restore SKYOpt Defaults")]
    static void RestoreSkyOptDefault()
    {
        /* //This destroys scene camera :/
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            Object.DestroyImmediate(obj);
        }*/
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("HumanFigurine"))
        {
            Object.DestroyImmediate(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("UserLocation"))
        {
            Object.DestroyImmediate(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("NoiseLocation"))
        {
            Object.DestroyImmediate(obj);
        }
        if (GameObject.Find("CSVImporter") != null)
        {
            Object.DestroyImmediate(GameObject.Find("CSVImporter"));
        }
        new GameObject("CSVImporter", new System.Type[] { typeof(CSVImports) });
        if (GameObject.Find("Settings") != null)
        {
            Object.DestroyImmediate(GameObject.Find("Settings"));
        }
        new GameObject("Settings", new System.Type[] { typeof(LayerSettings) });
        if (GameObject.Find("CFDOrigin") != null)
        {
            Object.DestroyImmediate(GameObject.Find("CFDOrigin"));
        }
        new GameObject("CFDOrigin");
    }

    [MenuItem("SKYOpt/Clean AssetBundle Cache")]
    static void CleanAssetBundleCache()
    {
        Caching.CleanCache();
    }

    public static void ExportResource(int mode, string path) //0=windows, 1=android
    {
        // Bring up save panel
        //string basename = Selection.activeObject ? Selection.activeObject.name : "New Resource";

        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

            if (mode == 0)
            {
                // for Windows
                BuildPipeline.BuildAssetBundles(path,
                    BuildAssetBundleOptions.ForceRebuildAssetBundle,
                    BuildTarget.StandaloneWindows64);
            }

            if (mode == 1)
            {
                // for Android
                BuildPipeline.BuildAssetBundles(path,
                    BuildAssetBundleOptions.ForceRebuildAssetBundle,
                    BuildTarget.Android);
            }

           // Selection.objects = selection;
        }
    }

    // [MenuItem("SKYOpt/Build AssetBundle From Selection - Track dependencies")]
    // static void ExportResource() {
    //   // Bring up save panel
    //   string path = EditorUtility.SaveFilePanel("Save Resources", "", "New Resource", "unity3d");
    //   if (path.Length != 0) {
    //     // Build the resource file from the active selection.
    //     Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
    //     BuildPipeline.BuildAssetBundle(Selection.activeObject,
    //                                    selection,
    //                                    path,
    //                                    BuildAssetBundleOptions.CollectDependencies |
    //                                    BuildAssetBundleOptions.CompleteAssets);
    //     Selection.objects = selection;
    //   }
    // }

    // [MenuItem("Assets/Build AssetBundle From Selection - No dependency tracking")]
    // static void ExportResourceNoTrack() {
    //   // Bring up save panel
    //   string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
    //   if (path.Length != 0) {
    //     // Build the resource file from the active selection.
    //     BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path);
    //   }
    // }

}