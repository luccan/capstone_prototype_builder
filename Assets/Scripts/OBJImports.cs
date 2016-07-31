using UnityEngine;
using System.Collections;

public class OBJImports : MonoBehaviour
{

    public GameObject WallsOBJ;
    public Texture2D WallsTexture;
    public GameObject FurnitureOBJ;
    public Texture2D FurnitureTexture;
    public GameObject ViewsOBJ;
    public Texture2D ViewsTexture;

    public void BuildWalls()
    {
        if (WallsOBJ)
        {
            Vector3 origin = new Vector3(0, 0, 0);
            GameObject wall = (GameObject)Instantiate(WallsOBJ, origin, Quaternion.identity);
            wall.name = "Walls";
            if (WallsTexture)
            {
                MeshRenderer[] wallrenders = wall.GetComponentsInChildren<MeshRenderer>();
                foreach (var r in wallrenders)
                {
                    r.material.shader = Resources.Load("SKYOpt Material", typeof(Shader)) as Shader;
                    r.material.mainTexture = WallsTexture;
                }
            }
        }

        if (FurnitureOBJ)
        {
            Vector3 origin = new Vector3(0, 0, 0);
            GameObject furniture = (GameObject)Instantiate(FurnitureOBJ, origin, Quaternion.identity);
            furniture.name = "Furniture";
            if (FurnitureTexture)
            {
                MeshRenderer[] furniturerenders = furniture.GetComponentsInChildren<MeshRenderer>();
                foreach (var r in furniturerenders)
                {
                    r.material.shader = Resources.Load("SKYOpt Material", typeof(Shader)) as Shader;
                    r.material.mainTexture = FurnitureTexture;
                }
            }
        }

        if (ViewsOBJ)
        {
            Vector3 origin = new Vector3(0, 0, 0);
            GameObject views = (GameObject)Instantiate(ViewsOBJ, origin, Quaternion.identity);
            views.name = "View";
            if (ViewsTexture)
            {
                MeshRenderer[] viewsrenders = views.GetComponentsInChildren<MeshRenderer>();
                foreach (var r in viewsrenders)
                {
                    r.material.shader = Resources.Load("SKYOpt Material", typeof(Shader)) as Shader;
                    r.material.mainTexture = ViewsTexture;
                }
            }
        }
    }
}
