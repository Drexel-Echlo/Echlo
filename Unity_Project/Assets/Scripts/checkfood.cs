using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkfood : MonoBehaviour {

    public int foodneed;
    public int foodcount;
    public GameObject gameManager;
    private gamecontrol gameScript;

    private GameObject[] list;
    private GameObject[] foodlist;

    // Use this for initialization
    void Start () {
        gameScript = gameManager.GetComponent<gamecontrol>();
        foodcount = 0;
    }
	
	// Update is called once per frame
	void Update () {

        /*foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        {

        }*/
        list = GameObject.FindGameObjectsWithTag("LightEmUp");

        foreach (GameObject item in list) {
            if (item.name == "Food(Clone)")
            {
                if ((item.transform.position - this.transform.position).magnitude <= 7)
                {
                    foodcount++;
                    item.gameObject.name = "Food(1)";
                }
       
            }
        }

        Debug.Log(this.transform.position);
        if (foodcount == foodneed)
        {
            gameScript.gameWin = true;
        }
	}

     void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            foodcount++;
            //Debug.Log(foodcount);
        }
    }
}
