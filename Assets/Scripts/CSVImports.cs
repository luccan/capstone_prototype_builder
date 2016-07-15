using UnityEngine;
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
            updateCsv();
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
    private void updateCsv()
    {
        if (CFD == null)
        {
            _csv = "";
            _csvhead = new List<List<string>>();
            return;
        }
        //standardize windows and unix line endings
        string normtext = CFD.text.Replace("\r\n", "\n"); //Replace("\r", "\n")
        string[] lines = normtext.Split('\n');

        string result = "";
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
                }
                if (idx != PMV_column) //do not add comma on last row
                    line += ",";
            }
            if (i != lines.Length - 1) //do not add break on last line
                result += line + "\n";

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

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
