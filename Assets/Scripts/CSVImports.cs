﻿using UnityEngine;
using System.Collections.Generic;

public class CSVImports : MonoBehaviour {

    private TextAsset _CFD;
    public TextAsset CFD
    {
        get
        {
            return _CFD;
        }
        set
        {
            _CFD = value;
            updateCFDFromCsv();
            drawPMVMap();
        }
    }
    public int start_row = 0;
    public int x_column=0;
    public int y_column=1;
    public int Vx_column=2;
    public int Vy_column=3;
    public int Vz_column=4;
    public int V_column = 5; //redundant
    public int t_column=6;
    public int PMV_column=7;

    private Dictionary<Vector2, double> PMVMap = null;

    private string _csv = "";
    public string csv
    {
        get
        {
            return _csv;
        }
    }
    private List<List<string>> _csvhead = new List<List<string>>();
    public List<List<string>> csvhead
    {
        get
        {
            return _csvhead;
        }
    }

    private List<string> csvheader = new List<string> {"X","Y","Vx","Vy","Vz","V","T","PMV"};
    private void updateCFDFromCsv()
    {
        if (CFD == null)
        {
            PMVMap = new Dictionary<Vector2, double>();
            _csv = "";
            _csvhead = new List<List<string>>();
            return;
        }
        //standardize windows and unix line endings
        string normtext = CFD.text.Replace("\r\n", "\n"); //Replace("\r", "\n")
        string[] lines = normtext.Split('\n');

        string result = "";
        PMVMap = new Dictionary<Vector2, double>();
        _csvhead = new List<List<string>>();
        _csvhead.Add(csvheader);

        int[] colidx = { x_column, y_column, Vx_column, Vy_column, Vz_column, V_column, t_column, PMV_column };
        for (int i = start_row; i < lines.Length; i++)
        {
            string line = "";
            string[] fields = lines[i].Split(',');
            foreach (int idx in colidx)
            {
                if (fields.Length > idx) //empty string if no such field exists
                {
                    line += fields[idx];
                } // else { line = ""; continue; } //omit line if line is faulty
                if (idx != PMV_column) //do not add comma on last row
                    line += ",";
            }
            if (i != lines.Length - 1) //do not add break on last line
                result += line + "\n";

            // populate PMVMap Dictionary
            float x, y, pmv = 0.0f;
            if (float.TryParse(fields[x_column], out x) &&
                float.TryParse(fields[y_column], out y) &&
                float.TryParse(fields[PMV_column], out pmv))
            {
                if (PMVMap.ContainsKey(new Vector2(x, y))) { PMVMap[new Vector2(x, y)] = pmv; }
                else { PMVMap.Add(new Vector2(x, y), pmv); }
            }

            // populate csvhead
            if (i < start_row + 10)
            {
                List<string> row = new List<string>(); //for head
                string[] elements = line.Split(',');
                foreach (string e in elements)
                {
                    row.Add(e);
                }
                _csvhead.Add(row);
            }
        }

        _csv = result;
    }

    private void drawPMVMap()
    {
        Transform CFDorigin = GameObject.Find("CFDOrigin").transform;
        List<Transform> tempList = new List<Transform>();
        foreach (Transform child in CFDorigin) { tempList.Add(child); }
        foreach (Transform child in tempList) { DestroyImmediate(child.gameObject); }
        //redraw
        if (PMVMap != null)
        {
            GameObject CFDPt = Resources.Load<GameObject>("Prefabs/CFDPointPrefab");
            foreach (Vector2 xy in PMVMap.Keys)
            {
                GameObject ob = GameObject.Instantiate(CFDPt);
                ob.transform.SetParent(CFDorigin);
                ob.name = string.Format("{0:f}:({1:g},{2:g})", PMVMap[xy], xy.x, xy.y);
                ob.transform.position = new Vector3(xy.x, 0.0f, xy.y);
                Light light = ob.GetComponent<Light>();
                float r, g, b = 0.0f;
                r = Mathf.Max(Mathf.Min((float)PMVMap[xy], 1.0f), 0.0f);
                g = 1.0f - Mathf.Min(Mathf.Abs((float)PMVMap[xy]), 1.0f);
                b = Mathf.Max(Mathf.Min((float)PMVMap[xy] * -1.0f, 1.0f), 0.0f);
                light.color = new Color(r, g, b);
                light.range = 0.2f;
            }
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
