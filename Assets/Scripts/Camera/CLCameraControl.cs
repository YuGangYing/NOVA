using UnityEngine;
using System.Collections;

public class CLCameraControl : MonoBehaviour
{
    public GameObject ZoomPoint;
    public int PlayerID;
    public Collider MoveArea;
    private Vector3 mCurGroundPoint;
	
void Start ()
{

	Application.targetFrameRate = 100;
}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mCurGroundPoint = GroundPoint();
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 offset = GroundPoint() - mCurGroundPoint;
            offset.y = 0;
            Vector3 newPosition = transform.position - offset;
            newPosition.x = Mathf.Clamp(newPosition.x, MoveArea.bounds.min.x, MoveArea.bounds.max.x);
            newPosition.z = Mathf.Clamp(newPosition.z, MoveArea.bounds.min.z, MoveArea.bounds.max.z);
            transform.position = newPosition;
        }
    }

    Vector3 GroundPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float angle = Vector3.Angle(ray.direction, Vector3.down);
        float rayLength = Mathf.Abs(ray.origin.y) / Mathf.Cos(angle * Mathf.Deg2Rad);
        Vector3 point = ray.GetPoint(rayLength);
        return point;
    }
}