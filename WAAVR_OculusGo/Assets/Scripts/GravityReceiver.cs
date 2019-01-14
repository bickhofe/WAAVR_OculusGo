using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReceiver : MonoBehaviour
{

    public Gravity gravity;

    void Start()
    {
        gravity = GameObject.Find("GravityCenter").GetComponent<Gravity>();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        //print("grav");
        gravity.ApplyGravity(transform);
    }
}
