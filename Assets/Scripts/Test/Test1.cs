using UnityEngine;
using System.Collections;

public class Test1 : MonoBehaviour {

	public MoveAI moveAI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit)){
				Vector3 tar = hit.point;
				GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
				go.transform.position = tar;
				moveAI.Move(tar);
			}
		}
	}
}