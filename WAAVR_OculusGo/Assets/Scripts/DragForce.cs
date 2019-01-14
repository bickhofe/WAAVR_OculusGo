using UnityEngine;
using System.Collections;

public class DragForce : MonoBehaviour {

	public float dragForce = 0;

	public void ApplyForce(Transform receiver)
	{
		Rigidbody rb = receiver.GetComponent<Rigidbody>();
		Vector3 forceUp = receiver.position - transform.position;
		Vector3 dir;
		dir = dragForce * forceUp.normalized;

		rb.AddForce(dir);

		Vector3 receiverUp = receiver.up;
		Quaternion rot = Quaternion.FromToRotation(receiverUp, forceUp)*receiver.rotation;
		receiver.rotation = rot;
	}
}
