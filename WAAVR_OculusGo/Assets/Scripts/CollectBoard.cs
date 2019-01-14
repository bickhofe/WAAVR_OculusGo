using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectBoard : MonoBehaviour {
    GameManager MainScript;
    public Transform Item;
    
	// Use this for initialization
	void Start () {
        MainScript = GameObject.Find("Main").GetComponent<GameManager>();
	}

    public void GenerateBoard(){
        //clear
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        int count = 0;

        for (int i = 0; i< 5; i++){
            for (int j = 0; j< 8; j++){
                //offset
                Transform newItem = Instantiate(Item, Vector3.zero, Quaternion.identity);
                newItem.transform.parent = transform;
                newItem.transform.localRotation = Quaternion.identity;
                newItem.transform.position = transform.position+new Vector3(0,-.25f*j,-.5f*i);


                GameObject item;
                item = newItem.GetChild(i + 1).GetChild(j).gameObject;
                
                if (MainScript.collectibleList[count] > 0) {
                    //3d obj
                    item.SetActive(true);
                    newItem.transform.Find("Title/Canvas/hook").gameObject.SetActive(true);
                } else {
                    newItem.transform.Find("Box").gameObject.SetActive(true);
                }
                newItem.GetComponentsInChildren<Text>()[0].text = item.name;
                //else newItem.transform.Find("hook").gameObject.SetActive(false);

                newItem.GetComponentsInChildren<Text>()[1].text = MainScript.collectibleList[count].ToString();
                count++;
            }
        }
    }
	
}
