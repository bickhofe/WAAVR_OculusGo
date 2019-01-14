using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {

	public GameManager MainScript;
	//float gravityForce = 0;

	public void ApplyGravity(Transform receiver)
	{
		Rigidbody rb = receiver.GetComponent<Rigidbody>();
		Vector3 forceUp = receiver.position - transform.position;
		Vector3 dir;
		dir = MainScript.gravityForce * forceUp.normalized;

		rb.AddForce(dir);

		Vector3 receiverUp = receiver.up;
		Quaternion rot = Quaternion.FromToRotation(receiverUp, forceUp)*receiver.rotation;
		receiver.rotation = rot;
	}
}
