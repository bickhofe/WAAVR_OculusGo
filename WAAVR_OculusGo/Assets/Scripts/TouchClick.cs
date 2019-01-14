using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchClick : MonoBehaviour {

	public int numOfTouches;
	int initTouches;
	int distance;

	Ray ray ;
	RaycastHit hit;
	
	Text ResetText;	
	Text DistText;
	string DistTXT;

	void Start(){

		initTouches = numOfTouches;
		ResetText = GameObject.Find ("Reset").GetComponentInChildren<Text>();
		ResetText.text = "tab on earth ("+numOfTouches+" times) to reset\nhighscores";
		//DistText = GameObject.Find ("HudDistance").GetComponentInChildren<Text>();

		if (PlayerPrefs.GetInt("HUD")!= null) distance = PlayerPrefs.GetInt("HUD");
		Debug.Log (distance);
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton(0)){
			Application.LoadLevel("Universe");
		}

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				if (Physics.Raycast(ray, out hit, 100.0F))
				{
					Debug.Log(hit.transform);
					if (hit.transform.name == "Title"){
						numOfTouches = 0;
						Application.LoadLevel("Universe");
					}

					if (hit.transform.name == "Earth"){
						if (numOfTouches > 1) {
							numOfTouches--;
							ResetText.text = "tab on earth ("+numOfTouches+" times) to\nreset highscores";
						} else {
							PlayerPrefs.SetInt("score", 0);
							ResetText.text = "Highscore was reset to 0!";
							numOfTouches = initTouches;
						}
					}

					if (hit.transform.name == "Playbook"){
						numOfTouches = 0;
						Application.OpenURL("https://play.google.com/store/apps/details?id=air.A20131203dkaandroid");
					}

					if (hit.transform.name == "Astrolander"){
						numOfTouches = 0;
						Application.OpenURL("https://play.google.com/store/apps/details?id=com.bickhofe.astrolander");
					}

					if (hit.transform.name == "HUD_Dist"){
						if (distance < 3) distance++;
						else distance = 0;

						PlayerPrefs.SetInt("HUD", distance);

						if (distance == 0) DistTXT = "Default";
						else if (distance == 1) DistTXT = "Offset +0.25";
						else if (distance == 2) DistTXT = "Offset +0.5";
						else if (distance == 3) DistTXT = "Offset +0.75";

						DistText.text = "HUD distance:\n";

					}

				}
			}
		}
	}
}
