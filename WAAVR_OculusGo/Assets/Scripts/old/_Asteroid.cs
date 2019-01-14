using UnityEngine;
using System.Collections;

public class _Asteroid : MonoBehaviour {

	public bool isTarget;

	// Use this for initialization
	void Start () {
		isTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, new Vector3(0,0,0));
	}
}
