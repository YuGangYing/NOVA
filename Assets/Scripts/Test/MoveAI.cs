using UnityEngine;
using System.Collections;
using Pathfinding;

public class MoveAI : MonoBehaviour {

	Seeker seeker;

	// Use this for initialization
	void Start () {
		//Get a reference to the Seeker component we added earlier        
	    seeker = GetComponent<Seeker>();                
		//Start a new path to the targetPosition, return the result to the OnPathComplete function        

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPathComplete(Path p){
		Debug.Log("Moving");
	}

	public void Move(Vector3 target){
		seeker.StartPath (transform.position,target, OnPathComplete);
	}

}
