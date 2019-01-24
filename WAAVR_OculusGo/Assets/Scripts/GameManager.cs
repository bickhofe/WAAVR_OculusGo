using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // public GameObject TestCube;
    // public Text DebugText;

    public Transform VRCam;
    //public bool dof6 = true;  
    public GameObject Player;
    public GameObject Title;
    public GameObject Status;
    public GameObject NewItemInfo;
    public Text ammoText;
    public Text scoreText;
    public Text gravityText;
    public Text shieldText;
    public Text highscoreText;
    public Text lastScoreText;

    public CollectBoard BoardScript;
    public int[] collectibleList;
    public GameObject SmartBombText;
    public string gameState = "Title";
    public bool pause = true;
    public GameObject Asteroid;
    public GameObject AsteroidMed;
    public GameObject AsteroidSmall;

    public GameObject AsteroidSpecial0;
    public GameObject AsteroidSpecial1;
    public GameObject AsteroidSpecial2;

    bool easy = false;
    bool med = false;
    bool hard = false;

    public GameObject AsteroidContainer;

    public GameObject Collectible;
    public GameObject CollectibleContainer;

    public GameObject NearestAsteroid;
    public float spawnTimeAsteroid = 20;

    public Goodie GoodieScript;
    public float spawnTimeGoodie = 15;

    public float hitTimeCount = 0;
    public int hitCount = 0;
    public int hitTimeFrame = 2;
    public Vector3 lastHitPosition;

    public float gravityRise = .01f;
    public GameObject rocketPrefab;

    public int Score = 0;
    public int Energy = 100;
    public int Ammo = 25;
    public bool SmartBomb = false;
    public ParticleSystem SmartBombFX;
    public float SmartLoad = 0;

    public float gravityForce = 0;
    public MySoundFX SoundScript;


    // Use this for initialization
    void Start()
    {
        collectibleList = new int[40];
        NearestAsteroid = null;

        if (PlayerPrefs.HasKey("Highscore")) highscoreText.text = "" + PlayerPrefs.GetInt("Highscore");
        else highscoreText.text = "00000";
        //print(collectibleList);
        for (int i = 0; i < 40; i++) collectibleList[i] = 0;

        if (PlayerPrefs.HasKey("ColList"))
        {
            print(PlayerPrefs.GetString("ColList"));

            for (int i = 0; i < 40; i++)
            {
                string colList = PlayerPrefs.GetString("ColList");
                string myChar = colList.Substring(i, 1);
                collectibleList[i] = int.Parse(myChar);
            }
        }

        BoardScript.GenerateBoard();

    }

    public void ResetScores()
    {
        PlayerPrefs.DeleteAll();
        lastScoreText.text = "00000";
        highscoreText.text = "00000";
        collectibleList = new int[40];
        BoardScript.GenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "Title")
        {
            if (!Title.activeSelf) Title.SetActive(true);
            if (Status.activeSelf) Status.SetActive(false);

            //if (Input.GetMouseButtonDown(0)) StartGame();
            //if (Input.GetMouseButtonDown(1)) ResetScores();
        }

        if (gameState == "Game")
        {
            ammoText.text = "" + Ammo;
            scoreText.text = "" + Score;
            float grav = gravityForce * -10;
            gravityText.text = grav.ToString("F2");
            shieldText.text = "" + Energy;

            //show Smartbomb info
            if (SmartBomb) if (!SmartBombText.activeSelf) SmartBombText.SetActive(true);

            //faster goodies when ammo is low
            if (Ammo < 3) spawnTimeGoodie = 5;
            else spawnTimeGoodie = 10;

            if (Title.activeSelf) Title.SetActive(false);
            if (!Status.activeSelf) Status.SetActive(true);
            
            FindNearestTarget();

            //hit time counter
            if (hitTimeCount > 0)
            {
                hitTimeCount -= Time.deltaTime;

                if (hitCount == 4 && easy == false)
                {
                    CreateSpecialRoidEasy(lastHitPosition);
                    easy = true;
                    hitTimeCount++;
                    print("easy");
                }

                else if (hitCount == 5 && med == false)
                {
                    CreateSpecialRoidMed(lastHitPosition);
                    med = true;
                    hitTimeCount++;
                    print("med");
                }

                else if (hitCount >= 6 && hard == false)
                {
                    CreateSpecialRoidHard(lastHitPosition);
                    hard = true;
                    print("hard");
                }
            }
            else

            {
                hitTimeCount = 0;
                hitCount = 0;
                easy = med = hard = false;
                //print("ende");
            }

            // Game over
            if (Energy <= 0)
            {
                GameOver();
            }
        }
    }

    public void LoadSmartBomb()
    {
        if (SmartBomb)
        {
            //play sound
            if (SmartLoad == 0) SoundScript.audioTimer.Play();
            SmartLoad += Time.deltaTime;

            if (SmartLoad >= 3f) FireSmartBomb();
        }
    }

    public void StopLoadingSmartBomb()
    {
        if (SmartLoad > 0)
        {
            SmartLoad = 0;
            SoundScript.audioTimer.Stop();
        }
    }

    public void StartGame()
    {
        gameState = "Game";
        SmartBomb = true;
        Ammo = 30;
        pause = false;
        Score = 0;
        Energy = 100;
        gravityForce = -.1f;

        //create first target and goodies
        InvokeRepeating("CreateBigTarget", 0, spawnTimeAsteroid);

        //goodie
        GoodieScript.HideGoodie();

        //Sound
        SoundScript.audioFX.Stop();
        SoundScript.audioFX.PlayOneShot(SoundScript.gameStart);
        SoundScript.audioLoop.Play();

        //speech
        SoundScript.Speech("Start");
    }

    void CreateBigTarget()
    {
        GameObject newAsteroid = Instantiate(Asteroid, Vector3.zero, Quaternion.identity) as GameObject;
        newAsteroid.GetComponent<Asteroid>().type = "big";
        newAsteroid.transform.parent = AsteroidContainer.transform;

        // Teleport randomly.
        Vector3 direction = Random.insideUnitCircle.normalized;
        float height = Random.Range(.0f, .1f);
        Vector3 rndDir = new Vector3(direction.x, height, direction.y);
        float distance = 20;

        newAsteroid.transform.localPosition = rndDir * distance;

        SoundScript.Speech("Warning");
    }

    public void CreateMediumTarget(Vector3 position)
    {
        int rnd = Random.Range(2, 4);

        for (int i = 0; i < rnd; i++)
        {
            GameObject newAsteroid = Instantiate(AsteroidMed, position, Quaternion.identity) as GameObject;
            newAsteroid.GetComponent<Asteroid>().type = "medium";
            newAsteroid.transform.parent = AsteroidContainer.transform;
        }
    }

    public void CreateSmallTarget(Vector3 position)
    {
        int rnd = Random.Range(2, 4);

        for (int i = 0; i < rnd; i++)
        {
            GameObject newAsteroid = Instantiate(AsteroidSmall, position, Quaternion.identity) as GameObject;
            newAsteroid.GetComponent<Asteroid>().type = "small";
            newAsteroid.transform.parent = AsteroidContainer.transform;
        }
    }

    public void CreateSpecialRoidEasy(Vector3 position)
    {
        GameObject newAsteroid = Instantiate(AsteroidSpecial0, position, Quaternion.identity) as GameObject;
        newAsteroid.GetComponent<Asteroid>().type = "special0";
        newAsteroid.transform.parent = AsteroidContainer.transform;
        SoundScript.Speech("Warning");
    }

    public void CreateSpecialRoidMed(Vector3 position)
    {
        GameObject newAsteroid = Instantiate(AsteroidSpecial1, position, Quaternion.identity) as GameObject;
        newAsteroid.GetComponent<Asteroid>().type = "special1";
        newAsteroid.transform.parent = AsteroidContainer.transform;
        SoundScript.Speech("Warning");
    }

    public void CreateSpecialRoidHard(Vector3 position)
    {
        GameObject newAsteroid = Instantiate(AsteroidSpecial2, position, Quaternion.identity) as GameObject;
        newAsteroid.GetComponent<Asteroid>().type = "special2";
        newAsteroid.transform.parent = AsteroidContainer.transform;
        SoundScript.Speech("Warning");
    }

    public void CreateCollectible(Vector3 position, int level)
    {
        GameObject newCollectible = Instantiate(Collectible, position, Quaternion.identity) as GameObject;
        newCollectible.GetComponent<Collectible>().level = level;
        newCollectible.transform.parent = CollectibleContainer.transform;
    }

    public void GameOver()
    {
        gameState = "Title";
        
        //stop spawning asteroids
        CancelInvoke("CreateBigTarget");
        CancelInvoke("CreateGoodie");

        //delete all asteroids left
        foreach (Transform child in AsteroidContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform item in CollectibleContainer.transform)
        {
            Destroy(item.gameObject);
        }

        GoodieScript.HideGoodie();

        //play sound
        SoundScript.audioFX.PlayOneShot(SoundScript.gameOver);

        //disable gravity
        gravityForce = 0;

        SoundScript.audioFX.PlayOneShot(SoundScript.gameStart);
        SoundScript.audioLoop.Stop();

        //update item board
        //print(collectibleList.ToString());
        string list = "";
        for (int i = 0; i < collectibleList.Length; i++) list += collectibleList[i];

        PlayerPrefs.SetString("ColList", list);
        BoardScript.GenerateBoard();

        //update highscore
        lastScoreText.text = "" + Score;
        highscoreText.text = "00000";

        //if (!PlayerPrefs.HasKey("Highscore")) return;

        if (Score > PlayerPrefs.GetInt("Highscore")) PlayerPrefs.SetInt("Highscore", Score);
        highscoreText.text = "" + PlayerPrefs.GetInt("Highscore");
    }

    public void FireRocket(GameObject target)
    {
        //print("fire");
        Asteroid AsteroidScript = target.GetComponent<Asteroid>();

        Ammo--;

        //rocket
        Quaternion startAngle;
        startAngle = Random.rotation; // random rotation
        GameObject myRocket = Instantiate(rocketPrefab, Player.transform.position + Player.transform.forward * .1f, startAngle) as GameObject;
        Rocket RocketScript = myRocket.GetComponent<Rocket>();
        RocketScript.target = target;

        //fx
        SoundScript.audioFX.PlayOneShot(SoundScript.lockTarget);
        SoundScript.audioFX.PlayOneShot(SoundScript.rocketStart);
    }

    public void FireSmartBomb()
    {
        //controller rumble fx
        //WaveVR_Controller.Input (device).TriggerHapticPulse ();

        SmartBombText.SetActive(false);
        SmartBomb = false;
        SmartLoad = 0;
        SoundScript.audioFX.PlayOneShot(SoundScript.smartBomb);

        foreach (Transform roid in AsteroidContainer.transform)
        {
            float rnd = Random.value / 2;
            roid.gameObject.GetComponent<Asteroid>().Invoke("RocketExplosionFX", rnd);
            Destroy(roid.gameObject, rnd);
        }

        SmartBombFX.Play();
    }

    void FindNearestTarget()
    {
        float dist = 100;

        foreach (Transform roid in AsteroidContainer.transform)
        {
            float roidDist = Vector3.Distance(roid.transform.position, Vector3.zero);
            if (roidDist < dist)
            {
                dist = roidDist;
                NearestAsteroid = roid.gameObject;
            }
        }
    }

    public void AddCollectible(int id, int type, int item)
    {

        //reset items
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 8; j++) NewItemInfo.transform.GetChild(i + 1).GetChild(j).gameObject.SetActive(false);
        }

        if (collectibleList[id] == 0)
        {
            collectibleList[id]++;
            NewItemInfo.SetActive(true);
            GameObject itemFound = NewItemInfo.transform.GetChild(type + 1).GetChild(item).gameObject;
            itemFound.SetActive(true);
            SoundScript.audioFX.PlayOneShot(SoundScript.yeehaa);

            NewItemInfo.transform.GetComponentsInChildren<Text>()[0].text = itemFound.name;

            CancelInvoke("HidenewItemInfo");
            Invoke("HidenewItemInfo", 5);
        }

        Score += 100 / (type + 1) + (item + 1) * 10;
        SoundScript.audioFX.PlayOneShot(SoundScript.coin);
    }

    void HidenewItemInfo()
    {
        NewItemInfo.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
