using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
//using HTC.UnityPlugin.Vive;

[RequireComponent(typeof(Collider))]

public class Asteroid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler

{
    private HashSet<PointerEventData> hovers = new HashSet<PointerEventData>();
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hovers.Add(eventData) && hovers.Count == 1)
        {
            FocusTarget();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hovers.Remove(eventData) && hovers.Count == 0)
        {
            BlurTarget();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // if (eventData.IsViveButton(ControllerButton.Trigger))
        // {
        //     // Vive button triggered!
        //     LockTarget();
        // }
        // else if (eventData.button == PointerEventData.InputButton.Left)
        // {
        //     // Standalone button triggered!
        // }
    }
    
    GameManager MainScript;
    Gravity gravityScript;

    public string type = ""; 

    public int minDist = 20;
    public bool isLocked = false;

    //target
    public GameObject Target;
    public Text TargetTxt;
    public GameObject RocketExplosion;
    public GameObject RocketExplosionBig;
    public GameObject ImpactExplosion;
    public AudioClip newBigRoid;
    public AudioClip newSpecial;
    //ReticlePoser Pointer;

    void Start()
    {
        MainScript = GameObject.Find("Main").GetComponent<GameManager>();
        gravityScript = GameObject.Find("GravityCenter").GetComponent<Gravity>();
        Target.SetActive(false);

        //Pointer = new ReticlePoser();

        //set size
        if (type == "big") {
            transform.localScale = new Vector3(3, 3, 3);
            GetComponent<AudioSource>().PlayOneShot(newBigRoid);
        }
        else if (type == "medium") {
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * 25);
        }
        else if (type == "small") {
            transform.localScale = new Vector3(.33f, .33f, .33f);
            GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * 25);
        }

        else if (type == "special0") GetComponent<AudioSource>().PlayOneShot(newSpecial);
        else if (type == "special1") GetComponent<AudioSource>().PlayOneShot(newSpecial);
        else if (type == "special2") GetComponent<AudioSource>().PlayOneShot(newSpecial);


    }

    void Update()
    {
        if (MainScript.gameState == "Game")
        {
            //target ring position und ausrichtung auf die vr camera
            Target.transform.LookAt(MainScript.VRCam.transform.position);

            //if (Input.GetMouseButtonDown(1)) LockTarget();
        }
    }

    public void LockTarget()
    {
        if (MainScript.Ammo > 0) MainScript.FireRocket(gameObject);
        else MainScript.SoundScript.audioFX.PlayOneShot(MainScript.SoundScript.empty);
    }

    public void FocusTarget()
    {
        if (!isLocked) Target.SetActive(true);
    }

    public void BlurTarget()
    {
        if (!isLocked) Target.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rocket")
        {
            RocketExplosionFX();
            MainScript.gravityForce -= MainScript.gravityRise;

            //create child asteroids
            if (type == "big") MainScript.CreateMediumTarget(transform.position);
            else if (type == "medium") MainScript.CreateSmallTarget(transform.position);
            else if (type == "special0") MainScript.CreateCollectible(transform.position, 0);
            else if (type == "special1") MainScript.CreateCollectible(transform.position, 1);
            else if (type == "special2") MainScript.CreateCollectible(transform.position, 2);

            //von neuem an zählen
            if (MainScript.hitCount == 0) MainScript.hitTimeCount = MainScript.hitTimeFrame;
            MainScript.hitCount++; //hitcount
            MainScript.lastHitPosition = transform.position;

            MainScript.SoundScript.Speech("Success");
            
            Destroy(gameObject);
        }

        if (other.name == "GravityCenter")
        {
            Instantiate(ImpactExplosion, transform.position, Quaternion.identity);
            
            if (type == "big") MainScript.Energy -= 45;
            else if (type == "medium") MainScript.Energy -= 30;
            else if (type == "small") MainScript.Energy -= 15;

            if (MainScript.Energy < 25) MainScript.SoundScript.Speech("Shield");
            else MainScript.SoundScript.Speech("Hurt");
             
            Destroy(gameObject);
        }
    }

    public void RocketExplosionFX(){
        MainScript.Score += 100;
        if (type == "big") Instantiate(RocketExplosionBig, transform.position, Quaternion.identity);
        else Instantiate(RocketExplosion, transform.position, Quaternion.identity);
    }

}
