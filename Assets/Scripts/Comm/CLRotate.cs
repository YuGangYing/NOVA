using UnityEngine;
using System.Collections;

public class CLRotate : MonoBehaviour
{
    public Vector3 Speed;

    void Update()
    {
        transform.Rotate(Speed * Time.deltaTime);
    }
}