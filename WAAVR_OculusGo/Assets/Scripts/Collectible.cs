using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{

    public Transform TitleObj;
    public Text Title;
    GameManager MainScript;
    public int level = 0;
    public Transform[] Monsters;
    public Transform[] Tools;
    public Transform[] Sweets;
    public Transform[] Food;
    public Transform[] Things;
    int id;
    int colType = 4;
    int rndItem;

    GameObject curItem;

    // Use this for initialization
    void Start()
    {
        MainScript = GameObject.Find("Main").GetComponent<GameManager>();

        ResetItems();

        switch (level)
        {
            case 0:
                colType = Random.Range(4, 1);
                break;
            case 1:
                colType = 1;
                break;
            case 2:
                colType = 0;
                break;
            default:
                break;
        }

        rndItem = Random.Range(0, 8);
        if (colType == 0) curItem = Monsters[rndItem].gameObject;
        else if (colType == 1) curItem = Sweets[rndItem].gameObject;
        else if (colType == 2) curItem = Food[rndItem].gameObject;
        else if (colType == 3) curItem = Tools[rndItem].gameObject;
        else if (colType == 4) curItem = Things[rndItem].gameObject;

        id = colType * 8 + rndItem;

        curItem.SetActive(true);
        Title.text = curItem.name;
    }

    void ResetItems()
    {
        for (int j = 0; j < Monsters.Length; j++) Monsters[j].gameObject.SetActive(false);
        for (int j = 0; j < Sweets.Length; j++) Sweets[j].gameObject.SetActive(false);
        for (int j = 0; j < Food.Length; j++) Food[j].gameObject.SetActive(false);
        for (int j = 0; j < Tools.Length; j++) Tools[j].gameObject.SetActive(false);
        for (int j = 0; j < Things.Length; j++) Things[j].gameObject.SetActive(false);
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "GravityCenter")
        {
            MainScript.AddCollectible(id, colType, rndItem);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //look at
        Quaternion rot = new Quaternion();
        rot = Quaternion.LookRotation(MainScript.VRCam.position - TitleObj.position);
        TitleObj.rotation = Quaternion.Slerp(TitleObj.rotation, rot, 5 * Time.deltaTime);
    }
}
