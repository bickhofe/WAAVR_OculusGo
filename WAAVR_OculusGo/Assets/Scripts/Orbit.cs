using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    Vector3 randomRotation;

    public float speed = 25;
    float xSpeed;
    float ySpeed;
    float zSpeed;

    // Use this for initialization
    void Start()
    {
        transform.rotation = Random.rotation;

        xSpeed = Random.value * speed;
        ySpeed = Random.value * speed;
        zSpeed = Random.value * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime);
    }
}
