using UnityEngine;
using System.Collections;

public class DebugRay : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
		Debug.DrawRay (transform.position, forward, Color.green);
	}
}
