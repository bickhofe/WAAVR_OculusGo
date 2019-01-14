using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Goodie : MonoBehaviour
{
    public GameManager MainScript;
    public Transform GravityCenter;
    MySoundFX SoundScript;

    public int minDist = 10;
    public int goodieType;
    public GameObject[] goodies;
    public GameObject curItem;
    bool isActive = false;
    float dist;

    Rigidbody rb;

    void Start()
    {
        MainScript = GameObject.Find("Main").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        SoundScript = MainScript.SoundScript;
    }

    void Update()
    {
        if (isActive) transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime / 0.5f);

        //check collision over distance
        dist = Vector3.Distance(GravityCenter.position, transform.position);

        if (isActive && dist < .25f)
        {
            switch (goodieType)
            {
                case 0:
                    MainScript.Score += 100;
                    SoundScript.audioFX.PlayOneShot(SoundScript.coin);
                    SoundScript.Speech("Thankyou");
                    break;
                case 1:
                    MainScript.Score += 25;
                    MainScript.Energy += 25;
                    if (MainScript.Energy > 100) MainScript.Energy = 100;
                    SoundScript.Speech("Better");
                    SoundScript.audioFX.PlayOneShot(SoundScript.battery);
                    break;
                case 2:
                    MainScript.Score += 25;
                    MainScript.Ammo += 30;
                    if (MainScript.Ammo > 30) MainScript.Ammo = 30;
                    SoundScript.Speech("Thankyou");
                    SoundScript.audioFX.PlayOneShot(SoundScript.rockets);
                    break;
                case 3:
                    MainScript.Score += 25;
                    if (!MainScript.SmartBomb)
                    {
                        MainScript.SmartBomb = true;
                        SoundScript.Speech("Smart");
                    }
                    SoundScript.audioFX.PlayOneShot(SoundScript.cogwheel);
                    break;
                case 4:
                    MainScript.Score += 25;
                    MainScript.gravityForce = MainScript.gravityForce / 2;
                    if (MainScript.gravityForce > -0.1f) MainScript.gravityForce = -0.1f;
                    SoundScript.Speech("Better");
                    SoundScript.audioFX.PlayOneShot(SoundScript.screw);
                    break;
                default:
                    print("Incorrect intelligence level.");
                    break;
            }

            HideGoodie();
        }
    }

    public void NewGoodie()
    {
        rb.velocity = Vector3.zero;

        //print("new goodie");
        isActive = true;
        SoundScript.audioFX.PlayOneShot(SoundScript.activate);

        foreach (GameObject goodie in goodies) goodie.SetActive(false);

        int rndGoodie = Random.Range(0, 5);

        if (MainScript.Ammo > 0) goodieType = rndGoodie;
        else goodieType = 2;

        goodies[goodieType].SetActive(true);

        // Teleport randomly.
        Vector3 rndDir = Random.onUnitSphere * 1.25f;

        transform.localPosition = rndDir;
        Invoke("HideGoodie", 15);
    }

    public void HideGoodie()
    {
        isActive = false;

        //kill currently running invokes
        CancelInvoke("HideGoodie");

        //move goodie up in the sky
        transform.localScale = Vector3.zero;
        if (MainScript.gameState == "Game") Invoke("NewGoodie", MainScript.spawnTimeGoodie);
    }
}
