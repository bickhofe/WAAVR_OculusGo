using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Astronaut : MonoBehaviour
{

    public GameManager MainScript;
    public Vector3 titlePos;
    //= new Vector3(0,2,0);
    int curPos = 0;

    void Update()
    {
        if (MainScript.VRCam.transform.position.x < 0 && MainScript.VRCam.transform.position.z > 0) curPos = 2;
        else if (MainScript.VRCam.transform.position.x > 0 && MainScript.VRCam.transform.position.z > 0) curPos = 3;
        else if (MainScript.VRCam.transform.position.x > 0 && MainScript.VRCam.transform.position.z < 0) curPos = 0;
        else if (MainScript.VRCam.transform.position.x < 0 && MainScript.VRCam.transform.position.z < 0) curPos = 1;

        Quaternion targetRotation = new Quaternion();
        

        // im pause mode schaut der astronaut immer richtung camera. ingame richtung asteroid
        if (MainScript.gameState == "Title")
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, titlePos, 1 * Time.deltaTime);
            targetRotation = Quaternion.LookRotation(MainScript.VRCam.position - transform.position);
            //transform.LookAt(MainScript.Camera.transform.position);
        }
        else
        {
            Vector3 pos = (MainScript.VRCam.transform.position - Vector3.up * .25f) + MainScript.VRCam.transform.forward * 1.25f;
            transform.position = Vector3.Slerp(transform.position, pos, .5f * Time.deltaTime);

            //astronaut schaut immer in richtung des nächsten asteroids (falls welche da sind)
            if (MainScript.NearestAsteroid != null && MainScript.Ammo > 0)  targetRotation = Quaternion.LookRotation(MainScript.NearestAsteroid.transform.position - transform.position);
            else targetRotation = Quaternion.LookRotation(MainScript.VRCam.position - transform.position);
        }

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

        //status window
        Quaternion statusRotation = new Quaternion();
        statusRotation = Quaternion.LookRotation(MainScript.VRCam.position -  MainScript.Status.transform.position);
        MainScript.Status.transform.rotation = Quaternion.Slerp(MainScript.Status.transform.rotation, statusRotation, 5 * Time.deltaTime);
    }
}
