using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MyControllerScript : MonoBehaviour
{
    public GameManager MainScript;
    public Transform Pointer;
    GameObject currentGameObject;
    Asteroid AsteroidScript;

    // Use this for initialization
    void Update()
    {
        if (OVRInput.GetActiveController() == OVRInput.Controller.RTrackedRemote)
        {
            transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTrackedRemote);
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
        }
        else if (OVRInput.GetActiveController() == OVRInput.Controller.LTrackedRemote)
        {
            transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTrackedRemote);
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTrackedRemote);
        }

        if (OVRInput.Get(OVRInput.Button.One))
        {
            MainScript.LoadSmartBomb();
        } else {
            MainScript.StopLoadingSmartBomb();
        }

        // if (Input.GetKeyDown(KeyCode.F11))
        // {
        //     MainScript.StartGame();
        // }

        if (OVRInput.Get(OVRInput.Button.Back)) // || Input.GetKeyDown(KeyCode.F12))
        {
            MainScript.GameOver();
        } 
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            currentGameObject = hit.transform.gameObject;
            
            Pointer.gameObject.SetActive(true);
            Pointer.transform.position = hit.point;
            Pointer.transform.LookAt(hit.point + hit.normal);

            if (hit.collider.tag == "BtnStart")
            {
                if (OVRInput.GetDown(OVRInput.Button.One)) MainScript.StartGame();
            }

            if (hit.collider.tag == "BtnReset")
            {
                if (OVRInput.GetDown(OVRInput.Button.One)) MainScript.ResetScores();
            }

            if (hit.collider.tag == "BtnQuit")
            {
                if (OVRInput.GetDown(OVRInput.Button.One)) MainScript.QuitGame();
            }

            if (hit.collider.tag == "Asteroid")
            {
                if (AsteroidScript == null) {
                    AsteroidScript = currentGameObject.GetComponent<Asteroid>();
                    AsteroidScript.FocusTarget();
                }

                if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) 
                    AsteroidScript.LockTarget();
            }
        }
        else
        {
            Pointer.gameObject.SetActive(false);
            
            if (currentGameObject != null)
            {
                 if (currentGameObject.tag == "Asteroid") currentGameObject.GetComponent<Asteroid>().BlurTarget();
                currentGameObject = null;
                AsteroidScript = null;
            }
        }

    }
}