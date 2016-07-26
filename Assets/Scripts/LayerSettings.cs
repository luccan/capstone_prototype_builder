using UnityEngine;
using System.Collections;

public class LayerSettings : MonoBehaviour {

    public bool MaterialToggling = true;
    public bool MaterialDefaultValue = true;
    public bool FurnitureToggling = true;
    public bool FurnitureDefaultValue = true;
    public bool HumanToggling = true;
    public bool HumanDefaultValue = true;
    public bool FreeMovementToggling = true;
    public bool FreeMovementDefaultValue = false;
    public bool ViewToggling = true;
    public bool ViewDefaultValue = true;
    public bool NoiseToggling = true;
    public bool NoiseDefaultValue = true;

    public string csv
    {
        get
        {
            string ret = "";
            ret += string.Format("Material:{0}:{1}", MaterialToggling, MaterialDefaultValue);
            ret += string.Format("Furniture:{0}:{1}", FurnitureToggling, FurnitureDefaultValue);
            ret += string.Format("Human:{0}:{1}", HumanToggling, HumanDefaultValue);
            ret += string.Format("FreeMovement:{0}:{1}", FreeMovementToggling, FreeMovementDefaultValue);
            ret += string.Format("View:{0}:{1}", ViewToggling, ViewDefaultValue);
            ret += string.Format("Noise:{0}:{1}", NoiseToggling, NoiseDefaultValue);
            return ret;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
