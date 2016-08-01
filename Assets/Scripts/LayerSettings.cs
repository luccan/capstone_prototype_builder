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
	public float FreeMovementSpeed = 0.5f;
	public float LightIntensity = 1f;

    public string csv
    {
        get
        {
            string ret = "";
            ret += string.Format("Material:{0}:{1}\n", MaterialToggling, MaterialDefaultValue);
            ret += string.Format("Furniture:{0}:{1}\n", FurnitureToggling, FurnitureDefaultValue);
            ret += string.Format("Human:{0}:{1}\n", HumanToggling, HumanDefaultValue);
            ret += string.Format("FreeMovement:{0}:{1}\n", FreeMovementToggling, FreeMovementDefaultValue);
            ret += string.Format("View:{0}:{1}\n", ViewToggling, ViewDefaultValue);
            ret += string.Format("Noise:{0}:{1}\n", NoiseToggling, NoiseDefaultValue);
			ret += string.Format("MovementSpeed:{0}\n", FreeMovementSpeed);
			ret += string.Format("LightIntensity:{0}\n", LightIntensity);
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
