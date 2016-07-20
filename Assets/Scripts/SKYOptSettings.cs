using UnityEngine;
using System.Collections;

public class SKYOptSettings : MonoBehaviour {

	public bool enableBaseGeometryLayerToggle = true;
	public bool enableFurnitureLayerToggle = true;
	public bool enableMaterialLayerToggle = true;
	public bool enableViewLayerToggle = true;
	public bool enableHumanLayerToggle = true;
	public bool enableNoiseLayerToggle = true;
	public bool enableFreeRoamToggle = true;

	public AudioClip customNoiseAudio;

	public bool BaseGeometryLayerDefaultValue = true;
	public bool FurnitureLayerDefaultValue = true;
	public bool MaterialLayerDefaultValue = true;
	public bool ViewLayerDefaultValue = true;
	public bool HumanLayerDefaultValue = true;
	public bool NoiseLayerDefaultValue = true;
	public bool FreeRoamDefaultValue = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
