using UnityEngine;
using System.Collections;

public class MySoundFX : MonoBehaviour
{
    public AudioSource audioFX;
    public AudioSource audioLoop;
    public AudioSource audioSpeech;
    public AudioSource audioTimer;
    public AudioClip activate;
    public AudioClip menuLoop;
    public AudioClip gameLoop;
    public AudioClip rocketStart;
    public AudioClip lockTarget;
    public AudioClip coin;
    public AudioClip battery;
    public AudioClip rockets;
    public AudioClip cogwheel;
    public AudioClip screw;
    public AudioClip gameStart;
    public AudioClip gameOver;
    public AudioClip smartBomb;
    public AudioClip empty;
    public AudioClip yeehaa;

    //speech
    public float[] Startup;
    public float[] Warnings;
    public float[] Ammo;
    public float[] Hurt;
    public float[] Tipp;
    public float[] Success;
    public float[] Shield;
    public float[] Better;
    public float[] Smart;
    public float[] Thankyou;



    // Use this for initialization
    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        audioFX = audios[0];
        audioLoop = audios[1];
        audioSpeech = audios[2];

        audioLoop.PlayOneShot(menuLoop);
    }

    public void Speech(string type){
        //print("Speech");
        
        float cuePoint = 0;

        if (type == "Start") cuePoint = Startup[Random.Range(0,Startup.Length)];
        if (type == "Warnings") cuePoint = Warnings[Random.Range(0,Warnings.Length)];
        
        if (type == "Hurt") cuePoint = Hurt[Random.Range(0,Hurt.Length)];
        if (type == "Tipp") cuePoint = Tipp[Random.Range(0,Tipp.Length)];
        if (type == "Thankyou") cuePoint = Thankyou[Random.Range(0,Thankyou.Length)];
        if (type == "Smart") cuePoint = Smart[Random.Range(0,Smart.Length)];
        if (type == "Better") cuePoint = Better[Random.Range(0,Better.Length)];

        if (Random.value < .75f) return;
        if (type == "Ammo") cuePoint = Ammo[Random.Range(0,Ammo.Length)];
        if (type == "Success") cuePoint = Success[Random.Range(0,Success.Length)];
        if (type == "Shield") cuePoint = Shield[Random.Range(0,Shield.Length)];
        
        

        audioSpeech.time = cuePoint;
        audioSpeech.Play();
        audioSpeech.SetScheduledEndTime(AudioSettings.dspTime+((cuePoint+2)-cuePoint));
    }

}
