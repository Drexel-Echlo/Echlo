using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFoodCount : MonoBehaviour {

    public int foodNeed = 3;
    public static int babyFood = 0;
    protected GameController gameScript;

    private GameObject[] list;
    //public List<GameObject> foodlist;


    // Use this for initialization
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

        int foodCount = 0;
        list = GameObject.FindGameObjectsWithTag("LightEmUp");

        foreach (GameObject item in list)
        {
            if (item.gameObject.layer == LayerMask.NameToLayer("Food") && Vector3.Distance(item.transform.position, this.transform.position) <= 7)
            {
                foodCount++;
                //foodlist.Add(item);
            }
        }
        foodCount += babyFood;
        if (foodCount >= foodNeed)
        {
            gameScript.gameWin = true;
        }
    }
}
