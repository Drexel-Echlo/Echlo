using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFood : MonoBehaviour {

    public int foodneed;
    public GameControl gameScript;

    private GameObject[] list;
    private GameObject[] foodlist;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        int foodcount = 0;
        list = GameObject.FindGameObjectsWithTag("LightEmUp");

        foreach (GameObject item in list) {
            if (item.gameObject.layer == LayerMask.NameToLayer("Food") && Vector3.Distance(item.transform.position, this.transform.position) <= 7)
            {
                foodcount++;
            }
        }

        if (foodcount >= foodneed)
        {
            gameScript.gameWin = true;
        }
	}
}
