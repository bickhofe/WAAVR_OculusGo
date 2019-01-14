using UnityEngine;
using System.Collections;

public class TouchRotate : MonoBehaviour {

	private GameObject inner;
	private float lookUpDown;
	private float lookLeftRight;
	public float rotationFactor = 1;
	
	// Use this for initialization
	void Start () {
		inner = GameObject.Find("Innerverse");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Moved)
			{
				lookUpDown = 0; //touch.deltaPosition.y;
				lookLeftRight = touch.deltaPosition.x;
			} else {
				lookUpDown = 0;
			}
			UpdateView ();
		}

		if (Input.GetMouseButtonDown(0))
		{	
			lookUpDown = Input.mousePosition.y;
			lookLeftRight = Input.mousePosition.x;
			UpdateView ();
		}
	}

	void UpdateView(){
		Vector3 myRotation = new Vector3(lookUpDown,lookLeftRight * -1,0);
		transform.Rotate(myRotation,Space.World); //outaverse
		inner.transform.Rotate(-myRotation,Space.World); //gegenbewegung innerverse (wegen skybox)
	}
}
