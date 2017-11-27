using UnityEngine;
using System.Collections;

public class CLGameObject
{
    /// <summary>
    /// Local
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GameObject Instantiate(GameObject prefab)
    {
        GameObject newObject = (GameObject)GameObject.Instantiate(prefab);
        newObject.name = prefab.name;
        newObject.transform.localPosition = prefab.transform.localPosition;
        newObject.transform.localRotation = prefab.transform.localRotation;
        return newObject;
    }

    public static MainComponentT Instantiate<MainComponentT>(GameObject prefab) where MainComponentT : MonoBehaviour
    {
        return Instantiate(prefab).GetComponent<MainComponentT>();
    }

    public static GameObject Instantiate(GameObject prefab, Transform worldPoint)
    {
        Object newObject = GameObject.Instantiate(
            prefab,
            worldPoint.position,
            worldPoint.rotation
            );
        newObject.name = prefab.name;
        return (GameObject)newObject;
    }

    public static GameObject Instantiate(string name)
    {
        Object emptyMech = Resources.Load(name);
        return Instantiate((GameObject)emptyMech);
    }

    public static GameObject Instantiate(string name, Transform worldPoint)
    {
        Object emptyMech = Resources.Load(name);
        return Instantiate((GameObject)emptyMech, worldPoint);
    }

    public static T AddComponent<T>(GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }
}