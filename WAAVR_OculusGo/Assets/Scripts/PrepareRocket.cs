using UnityEngine;
using System.Collections;

public class PrepareRocket : MonoBehaviour {
	
	public Gravity gravity;

	void Start () {
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.freezeRotation = true;
		gravity.ApplyGravity (transform);
	}
}
