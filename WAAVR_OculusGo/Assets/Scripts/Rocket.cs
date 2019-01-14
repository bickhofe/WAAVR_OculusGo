using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{
    public GameManager MainScript;

    private float rocketSpeed;
    private float aimSensitivity;
    public GameObject target;
    float dist;
    float intercept;
    public GameObject RocketExplosion;

    void Start()
    {
        MainScript = GameObject.Find("Main").GetComponent<GameManager>();
        rocketSpeed = 2 + MainScript.gravityForce * -2;
        dist = Vector3.Distance(Vector3.zero, target.transform.position);
    }

    void Update()
    {
        if (target != null)
        {
            intercept = (dist - Vector3.Distance(Vector3.zero, target.transform.position)) / dist;
            if (intercept<0) intercept = intercept *-1; //print(intercept);
            
            aimSensitivity = 0.025f + intercept / 10;

            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, aimSensitivity);

            transform.Translate(0, 0, rocketSpeed * Time.deltaTime, Space.Self); 
        }
        else
        {
            Instantiate(RocketExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}