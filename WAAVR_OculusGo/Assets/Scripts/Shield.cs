using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	public Color color;
	public Color hitColor;
	public Color lerpedColor;
	public Material ShieldColor;

	float timer = 1;
	public float speed = 5.0F;
	private float startTime;
	
	// Use this for initialization
	void Start () {
		//ShieldImpact ();
	}
	
	// Update is called once per frame
	void Update () {
		timer = (Time.time - startTime) * speed;

		if (timer < 1) {
			print(timer);
			lerpedColor = Color.Lerp(hitColor, color, timer);
			ShieldColor.SetColor("_EmissionColor", lerpedColor);
		}
	}

	public void ShieldImpact () {
		startTime = Time.time;
		ShieldColor.SetColor("_EmissionColor", hitColor);
	}

}
