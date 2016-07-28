using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class CSVImports : MonoBehaviour
{

    private TextAsset _CFD;
    public TextAsset CFD
    {
        get
        {
            return _CFD;
        }
        set
        {
            if (_CFD == value)
                return;
            _CFD = value;
            updateCFDFromCsv();
        }
    }
    private int _start_row = 0;
    public int start_row
    {
        get
        {
            return _start_row;
        }
        set
        {
            if (_start_row == value)
                return;
            _start_row = value;
            updateCFDFromCsv();
        }
    }
    private int _x_column = 0;
    public int x_column
    {
        get
        {
            return _x_column;
        }
        set
        {
            if (_x_column == value)
                return;
            _x_column = value;
            updateCFDFromCsv();
        }
    }
    private int _y_column = 1;
    public int y_column
    {
        get
        {
            return _y_column;
        }
        set
        {
            if (_y_column == value)
                return;
            _y_column = value;
            updateCFDFromCsv();
        }
    }
    private int _Vx_column = 3;
    public int Vx_column
    {
        get
        {
            return _Vx_column;
        }
        set
        {
            if (_Vx_column == value)
                return;
            _Vx_column = value;
            updateCFDFromCsv();
        }
    }
    private int _Vy_column = 4;
    public int Vy_column
    {
        get
        {
            return _Vy_column;
        }
        set
        {
            if (_Vy_column == value)
                return;
            _Vy_column = value;
            updateCFDFromCsv();
        }
    }
    private int _Vz_column = 5;
    public int Vz_column
    {
        get
        {
            return _Vz_column;
        }
        set
        {
            if (_Vz_column == value)
                return;
            _Vz_column = value;
            updateCFDFromCsv();
        }
    }
    private int _V_column = 6;
    public int V_column
    {
        get
        {
            return _V_column;
        }
        set
        {
            if (_V_column == value)
                return;
            _V_column = value;
            updateCFDFromCsv();
        }
    }
    private int _t_column = 7;
    public int t_column
    {
        get
        {
            return _t_column;
        }
        set
        {
            if (_t_column == value)
                return;
            _t_column = value;
            updateCFDFromCsv();
        }
    }
    private int _PMV_column = 8;
    public int PMV_column
    {
        get
        {
            return _PMV_column;
        }
        set
        {
            if (_PMV_column == value)
                return;
            _PMV_column = value;
            updateCFDFromCsv();
        }
    }

    private float _gridsize = 0.5f;
    public float gridsize
    {
        get
        {
            return _gridsize;
        }
        set
        {
            if (_gridsize == value)
                return;
            _gridsize = value;
            drawPMVMap();
        }
    }

    private bool _enableAdvancedFilter = false;
    public bool enableAdvancedFilter
    {
        get
        {
            return _enableAdvancedFilter;
        }
        set
        {
            _enableAdvancedFilter = value;
        }
    }

    private Vector2 _boundaryStart = new Vector2(-99999, -99999);
    public Vector2 boundaryStart
    {
        get { return _boundaryStart;  }
    }
    public float boundaryStartX
    {
        get
        {
            return _boundaryStart.x;
        }
        set
        {
            if (_boundaryStart.x == value)
                return;
            _boundaryStart.x = value;
            updateInfoFromCFDData();
        }
    }
    public float boundaryStartY
    {
        get
        {
            return _boundaryStart.y;
        }
        set
        {
            if (_boundaryStart.y == value)
                return;
            _boundaryStart.y = value;
            updateInfoFromCFDData();
        }
    }
    private Vector2 _boundaryEnd = new Vector2(99999, 99999);
    public Vector2 boundaryEnd
    {
        get { return _boundaryEnd; }
    }
    public float boundaryEndX
    {
        get
        {
            return _boundaryEnd.x;
        }
        set
        {
            if (_boundaryEnd.x == value)
                return;
            _boundaryEnd.x = value;
            updateInfoFromCFDData();
        }
    }
    public float boundaryEndY
    {
        get
        {
            return _boundaryEnd.y;
        }
        set
        {
            if (_boundaryEnd.y == value)
                return;
            _boundaryEnd.y = value;
            updateInfoFromCFDData();
        }
    }
    private int _maxpoints = 5000;
    public int maxpoints
    {
        get
        {
            return _maxpoints;
        }
        set
        {
            if (_maxpoints == value)
                return;
            _maxpoints = value;
            updateInfoFromCFDData();
        }
    }
    private float _maxVelocityFilter = 999.0f;
    public float maxVelocityFilter
    {
        get
        {
            return _maxVelocityFilter;
        }
        set
        {
            if (_maxVelocityFilter == value)
                return;
            _maxVelocityFilter = value;
            updateInfoFromCFDData();
        }
    }

    private Dictionary<Vector2, float[]> CFDData = null;
    private Dictionary<Vector2, float> PMVMap = null;

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

    //Read from cfd.csv
    private void updateCFDFromCsv()
    {
        EditorUtility.DisplayProgressBar("Working...", "Re-reading CSV. Please wait...", 0.0f);
        if (CFD == null)
        {
            CFDData = null;
            updateInfoFromCFDData();
            return;
        }
        //standardize windows and unix line endings
        string normtext = CFD.text.Replace("\r\n", "\n"); //Replace("\r", "\n")
        string[] lines = normtext.Split('\n');

        //                0           1           2           3       4           5           6       7
        int[] colidx = { x_column, y_column, Vx_column, Vy_column, Vz_column, V_column, t_column, PMV_column };

        CFDData = new Dictionary<Vector2, float[]>();
        for (int i = start_row; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(',');
            float[] data = new float[colidx.Length];
            bool valid = true;

            for (int j = 0; j < colidx.Length; j++)
            {
                int idx = colidx[j];
                if (fields.Length > idx) //empty string if no such field exists
                {
                    valid = (float.TryParse(fields[idx], out data[j]));
                }
                else
                    valid = false;
                if (!valid)
                    break;
            }

            if (valid)
            {
                Vector2 cor = new Vector2(data[0], data[1]);
                if (!CFDData.ContainsKey(cor)) //ignore if dict already contains data
                {
                    CFDData.Add(cor, data);
                }
            }
        }

        updateInfoFromCFDData();
    }

    private List<string> csvheader = new List<string> { "X", "Y", "Vx", "Vy", "Vz", "V", "T", "PMV" };
    private void updateInfoFromCFDData()
    {
        EditorUtility.DisplayProgressBar("Working...", "Filtering CFD Data. Please wait...", 0.4f);
        if (CFDData == null)
        {
            PMVMap = new Dictionary<Vector2, float>();
            _csv = "";
            _csvhead = new List<List<string>>();
            drawPMVMap();
            return;
        }

        Dictionary<Vector2, float[]> FilteredCFDData = new Dictionary<Vector2, float[]>();
        // filter data
        foreach (Vector2 key in CFDData.Keys)
        {
            if (CFDData[key][0] >= boundaryStart.x && CFDData[key][0] <= boundaryEnd.x && //x
                CFDData[key][1] >= boundaryStart.y && CFDData[key][1] <= boundaryEnd.y && //y
                CFDData[key][5] <= maxVelocityFilter) //v
            {
                FilteredCFDData.Add(key, CFDData[key]);
            }
            else
            {
                continue; //skip
            }
        }
        int skipfactor = Mathf.FloorToInt((float)FilteredCFDData.Count / (float)maxpoints);
        int skipcounter = 0;

        string result = "";
        PMVMap = new Dictionary<Vector2, float>();
        _csvhead = new List<List<string>>();
        _csvhead.Add(csvheader);

        foreach (Vector2 key in FilteredCFDData.Keys)
        {
            if (skipcounter != skipfactor) //if skipfactor == 0, add everything
            {
                skipcounter++; //skip until skipcounter==skipfactor
                continue;
            }
            skipcounter = 0; //reset counter

            string line = "";
            foreach (float data in FilteredCFDData[key])
            {
                if (line != "") //do not add comma on first row
                    line += ",";
                line += data.ToString();
            }
            if (result != "") //do not add break on first line
                result += "\n";
            result += line;

            // populate PMVMap Dictionary
            if (PMVMap.ContainsKey(new Vector2(FilteredCFDData[key][0], FilteredCFDData[key][1])))
            {
                PMVMap[new Vector2(FilteredCFDData[key][0], FilteredCFDData[key][1])] = FilteredCFDData[key][7];
            }
            else {
                PMVMap.Add(new Vector2(FilteredCFDData[key][0], FilteredCFDData[key][1]), FilteredCFDData[key][7]);
            }

            // populate csvhead
            if (_csvhead.Count < 10)
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
        drawPMVMap();
    }

    private void drawPMVMap()
    {
        EditorUtility.DisplayProgressBar("Working...", "Generating PMV Map. Please wait...", 0.8f);
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
                light.range = gridsize * 2.0F;
            }
        }
        UnityEditor.EditorUtility.ClearProgressBar();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
