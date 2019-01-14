using UnityEngine;
using System.Collections;

public class SelectTarget : MonoBehaviour {

	private RaycastHit hit;
	private Asteroid targetScript; //Asteroid Script

	public GameObject rocketPrefab;
	//private Vector3 startPosition;
	private Quaternion startAngle; // 0,90,90
	private GameObject Base;

	// debug 
	private TextMesh Output;

	void Start() {
		Output = GameObject.Find("Debug").GetComponent<TextMesh>();
		Base = GameObject.Find("Base");
		//startPosition = new Vector3(0f,0f,0f); //above earth (northpole) Vector3(0f,1.2f,0f)
	}

	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); // mouse
		//Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position); // touch

		if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Asteroid"){
			targetScript = hit.collider.gameObject.GetComponent<Asteroid>();
			//Output.text = ""+targetScript.isTarget;

			// jeder asteroid kann nur einmal beschossen werden
//			if (targetScript.isTarget == false)
//			{
//				targetScript.isTarget = true;
//				FireRocket(hit.collider.gameObject);
//			}	 
		}
	}

	void FireRocket (GameObject target)
	{
		// startAngle = Quaternion.LookRotation(target.transform.position - transform.position); //target direction
		startAngle = Random.rotation; // random rotation

		GameObject myRocket = Instantiate (rocketPrefab, Base.transform.position, startAngle) as GameObject;
		myRocket.GetComponent<Rocket>().target = target; //ziel zuweisen
		myRocket.transform.parent = transform; // mache rocket zu child von innerverse
		myRocket.transform.Translate(Vector3.forward * .5f);
		Debug.Log(Base.transform.position);
	}
}