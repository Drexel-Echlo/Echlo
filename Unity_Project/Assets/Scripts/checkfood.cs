using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFood : MonoBehaviour {

    public int foodNeed;
    public int foodCount;
    public GameControl gameScript;

    private GameObject[] list;
    private GameObject[] foodlist;

    // Use this for initialization
    void Start () {
        foodNeed = 3;
        foodCount = 0;
    }
	
	// Update is called once per frame
	void Update () {


        list = GameObject.FindGameObjectsWithTag("Home");

        foreach (GameObject item in list) {
            if (item.gameObject.layer == LayerMask.NameToLayer("Food") && Vector3.Distance(item.transform.position, this.transform.position) <= 7)
            {
                foodCount++;
            }
        }

        if (foodCount >= foodNeed)
        {
            gameScript.gameWin = true;
        }
	}
}
