using UnityEngine;
using System.Collections;

public class ForceReceiver : MonoBehaviour {
	//public Main MainScript;
	//private Asteroid mainScript;
	private DragForce forceCenterScript;
	private Goodie forceLookScript;
	private Rocket rocketScript;
	private Gravity gravityScript;

	SoundFX SoundScript;

	void Start () {
//		//mainScript = GameObject.Find("Asteroid").GetComponent<LockOnTarget>();
//		forceCenterScript = GameObject.Find("GravityCenter").GetComponent<DragForce>();
//		forceLookScript = GetComponent<Goodie>();
//		gravityScript = GameObject.Find("GravityCenter").GetComponent<Gravity>();
//		Rigidbody rb = GetComponent<Rigidbody>();
//		rb.useGravity = false; 
//		rb.freezeRotation = true;

		//sounds
		SoundScript = GameObject.Find ("Astronaut").GetComponent<SoundFX>();
	}

	void FixedUpdate () {
		//forceCenterScript.ApplyForce(transform);
	}
	
	//collision
	void OnTriggerStay(Collider collider)
	{
//		if (collider.gameObject.name == "GravityCenter")
//		{
//			//GameObject item = forceLookScript.curItem;
//			Debug.Log (item+" collected");
//
//			//transform.Find(item).GetComponent<Renderer>().enabled = false;
//			item.SetActive(false);
//			setGoodieValue(item.name);
//			forceLookScript.newGoodie();
//		}
	}

//	void setGoodieValue(string itemName){
//
//		//reset lock time
//		//mainScript.waitTime = 50;
//
//		if (itemName == "battery"){
//			//shield
//			MainScript.Score += 25;
//			//mainScript.waitTime = 25;
//			//mainScript.SendMessage("<!>\nBATTERY COLLECTED\nFAST LOCK: ON\n+25", 2);
//			SoundScript.audioFX.PlayOneShot(SoundScript.battery);
//		} else if (itemName == "wrench"){
//			//shield repair
//			MainScript.Score += 25;
//			if (MainScript.Energy < 100){
//				MainScript.Energy += 5;
//				if (MainScript.Energy > 100) MainScript.Energy = 100;
//				//mainScript.SendMessage("<!>\nWRENCH COLLECTED\nSHIELD: +5%\n+25", 2);
//			} else //mainScript.SendMessage("<!>\nWRENCH COLLECTED\nSHIELD: MAX\n+25", 2);
//			SoundScript.audioFX.PlayOneShot(SoundScript.wrench);
//		} else if (itemName == "screw"){
//			//aiming
//			MainScript.Score += 25;
//			//MainScript.gravityForce += mainScript.gravityRise/2;
//			//mainScript.SendMessage("<!>\nSCREW COLLECTED\nGRAVITY ADJUSTED: ON\n+25", 2);
//			SoundScript.audioFX.PlayOneShot(SoundScript.screw);
//		}
//	}
}
