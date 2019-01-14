using UnityEngine;
using System.Collections;

public class CreateAsteroid : MonoBehaviour {

	public GameObject asteroidPrefab;
	private int id;

	void Start() {
		//Invoke("NewAsteroid", 3);
		InvokeRepeating("NewAsteroid", 3, 3F);
	}

	void NewAsteroid() {
		float xPos = Random.Range (5f, 10F);
		if (Random.Range (0, 2) == 0) xPos = xPos *-1;


		float yPos = Random.Range (-3f, 3F);
		//float yPos = Random.Range (5f, 10F);
		//if (Random.Range (0, 2) == 0) yPos = yPos *-1;

		float zPos = Random.Range (5f, 10F);
		if (Random.Range (0, 2) == 0) zPos = zPos *-1;
		
		Vector3 randomPosition = new Vector3(xPos,yPos,zPos);
		//Transform childObject = Instantiate (asteroidPrefab, randomPosition, Quaternion.identity) as Transform;
		//childObject.parent = transform;

		GameObject myAsteroid = Instantiate (asteroidPrefab, randomPosition, Quaternion.identity) as GameObject;
		myAsteroid.transform.parent = transform;
		myAsteroid.transform.name = "Asteroid"+id;
		id++;
	}
}
