using UnityEngine;
using System.Collections;

public class Test01 : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent nav;
	public Transform target;

	// Use this for initialization
	void Start () {
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		nav.SetDestination(target.position);
	}
}
