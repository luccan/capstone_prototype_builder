using UnityEngine;
using System.Collections;

public class Step4 : MonoBehaviour {
    [TextArea(20,99)]
    public string Instructions = 
        "1. Go to Assets/resources/Prefabs" + 
        System.Environment.NewLine + " " + System.Environment.NewLine +
        "2. Drag & Drop UserLocation onto the scene" + 
        System.Environment.NewLine +
        "(Note: The first UserLocation placed will be the starting point)" + 
        System.Environment.NewLine + " " + System.Environment.NewLine +
        "3. Drag & Drop NoiseLocation onto the scene" +
        System.Environment.NewLine + " " + System.Environment.NewLine +
        "4. For each NoiseLocation placed, select one of the preset audio";
}
