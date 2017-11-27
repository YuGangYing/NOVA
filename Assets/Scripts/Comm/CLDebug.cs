using UnityEngine;
using System.Collections;

public class CLDebug
{
    public static void DrawCross(GameObject target, Color color)
    {
        Debug.DrawRay(target.transform.position, target.transform.forward, color);
        Debug.DrawRay(target.transform.position, -target.transform.forward, color);
        Debug.DrawRay(target.transform.position, target.transform.right, color);
        Debug.DrawRay(target.transform.position, -target.transform.right, color);
    }

    public static void DrawCross(Vector3 targetPosition, Color color)
    {
        Debug.DrawRay(targetPosition, Vector3.forward, color);
        Debug.DrawRay(targetPosition, -Vector3.forward, color);
        Debug.DrawRay(targetPosition, Vector3.right, color);
        Debug.DrawRay(targetPosition, -Vector3.right, color);
    }

    //public static void DrawMap()
    //{
    //    InitGrids(Width, Height);
    //    Mesh mapMesh = new Mesh();
    //    int gridCount = Width * Height;
    //    Vector3[] vertics = new Vector3[(Width + 1) * (Height + 1)];
    //    for (int z = 0; z < Height + 1; z++)
    //    {
    //        for (int x = 0; x < Width + 1; x++)
    //        {
    //            vertics[x + z * (Width + 1)] = new Vector3(x * GridWidth, 0, z * GridWidth);
    //        }
    //    }
    //    mapMesh.vertices = vertics;
    //    int[] triangles = new int[gridCount * 6];
    //    for (int z = 0; z < Height; z++)
    //    {
    //        for (int x = 0; x < Width; x++)
    //        {
    //            triangles[x * 6 + 0 + z * Width * 6] = x + z * (Width + 1);
    //            triangles[x * 6 + 1 + z * Width * 6] = x + 1 + (z + 1) * (Width + 1);
    //            triangles[x * 6 + 2 + z * Width * 6] = x + 1 + z * (Width + 1);
    //            triangles[x * 6 + 3 + z * Width * 6] = x + z * (Width + 1);
    //            triangles[x * 6 + 4 + z * Width * 6] = x + (z + 1) * (Width + 1);
    //            triangles[x * 6 + 5 + z * Width * 6] = x + 1 + (z + 1) * (Width + 1);
    //        }
    //    }
    //    mapMesh.triangles = triangles;
    //    GetComponent<MeshFilter>().mesh = mapMesh;
    //}
}