using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, new Vector3(3,3,5));
	}
}
