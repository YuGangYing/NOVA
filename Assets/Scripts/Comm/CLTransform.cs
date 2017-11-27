using UnityEngine;
using System.Collections;

public class CLTransform
{
    public static string GetHierarchyPath(Transform transform, Transform root)
    {
        string path = "";
        if (transform != root)
        {
            path = transform.name;
            Transform parent = transform.parent;
            while (parent != root)
            {
                path = parent.name + "/" + path;
                parent = parent.parent;
            }
        }
        return path;
    }

    public static void Attach(Transform parent, Transform child)
    {
        Vector3 localPosition = child.localPosition;
        Quaternion localRotation = child.localRotation;
        Vector3 localScale = child.localScale;

        child.parent = parent;

        child.localPosition = localPosition;
        child.localRotation = localRotation;
        child.localScale = localScale;
    }

    public static bool Interset(GameObject object1, float object1Radius, GameObject object2, float object2Radius)
    {
        return Interset(object1.transform.position, object1Radius, object2.transform.position, object2Radius);
    }

    public static bool Interset(Vector3 object1Pos, float object1Radius, Vector3 object2Pos, float object2Radius)
    {
        Vector3 distance = object1Pos - object2Pos;
        return object1Radius + object2Radius >= distance.magnitude;
    }

    public static bool IntersetIgnoreY(GameObject object1, float object1Radius, GameObject object2, float object2Radius)
    {
        return IntersetIgnoreY(object1.transform.position, object1Radius, object2.transform.position, object2Radius);
    }

    public static bool IntersetIgnoreY(Vector3 object1Pos, float object1Radius, Vector3 object2Pos, float object2Radius)
    {
        Vector3 distance = object1Pos - object2Pos;
        distance.y = 0;
        return object1Radius + object2Radius >= distance.magnitude;
    }
}