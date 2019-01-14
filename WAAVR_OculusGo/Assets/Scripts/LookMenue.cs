using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Collider))]

public class LookMenue : MonoBehaviour {
    //private CardboardHead head;
    private GameObject head;

    float aimCount = 0;
	public int waitTime = 5;
	bool isHit;
	public GameObject textTarget; 
	Text ButtonTxt;	
	public string textOutput; 

	Astronaut AstronautScript;
	//LockOnTarget GameStateScript;
	SoundFX SoundScript;
	bool playedOnce = false;
	
	void Start() {
        //head = Camera.main.GetComponent<StereoController>().Head;
        //head = GameObject.Find("VRCam");
		AstronautScript = GameObject.Find ("Astronaut").GetComponent<Astronaut>();
		//GameStateScript = GameObject.Find("Asteroid").GetComponent<LockOnTarget>();
		ButtonTxt = textTarget.GetComponentInChildren<Text>();
		ButtonTxt.text = textOutput;

		//sounds
		SoundScript = GameObject.Find ("Astronaut").GetComponent<SoundFX>();
	}
	
	void Update() {
//		if (GameStateScript.gameState == "pause") {
//			RaycastHit hit;
//       
//            //bool isHit = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
//
//            //Vector3 fwd = head.transform.TransformDirection(Vector3.forward);
//            //Debug.DrawRay(head.transform.position, fwd * 100, Color.green);
//
////			if (Physics.Raycast(head.transform.position, fwd, out hit)) {
////				if (hit.collider.gameObject.name == "MenuNewGame") isHit = true;
////            } else {
////                isHit = false;
////            }
//
//            if (isHit) {
//
//				//textfeld befüllen
//				ButtonTxt.text = textOutput+"\n\n"+(3-Mathf.Floor(aimCount));
//
//				if (!playedOnce) SoundScript.audioFX.PlayOneShot(SoundScript.activate);
//				playedOnce = true;
//				aimCount += Time.deltaTime;
//				
//				if (aimCount >= waitTime) {
//					//GameStateScript.gameState = "play";
//					aimCount = 0;
//					//AstronautScript.MoveIn();
//				}
//			} else {
//				aimCount = 0;
//				ButtonTxt.text = textOutput;
//				playedOnce = false;
//			}
//		}
	}
}
