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
            wall.AddComponent<MeshCollider>().sharedMesh = wall.transform.FindChild("default").GetComponent<MeshFilter>().sharedMesh;
            wall.name = "Walls";
            if (WallsTexture)
            {
                Material wallmat = Resources.Load("Material/WallMat", typeof(Material)) as Material;
                wallmat.mainTexture = WallsTexture;
                Renderer[] wallmesh = wall.GetComponentsInChildren<Renderer>();
                foreach (var r in wallmesh)
                {
                    r.GetComponent<Renderer>().material = wallmat;
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
                Material furmat = Resources.Load("Material/FurMat", typeof(Material)) as Material;
                furmat.mainTexture = FurnitureTexture;
                Renderer[] furmesh = furniture.GetComponentsInChildren<Renderer>();
                foreach (var r in furmesh)
                {
                    r.GetComponent<Renderer>().material = furmat;
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
                Material viewmat = Resources.Load("Material/ViewMat", typeof(Material)) as Material;
                viewmat.mainTexture = ViewsTexture;
                Renderer[] viewmesh = views.GetComponentsInChildren<Renderer>();
                foreach (var r in viewmesh)
                {
                    r.GetComponent<Renderer>().material = viewmat;
                }
            }
        }
    }
}
