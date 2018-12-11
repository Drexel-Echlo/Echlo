using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFoodCount : MonoBehaviour {

    protected GameController gameScript;

    public static int babyFood = 0;
    public int foodNeed = 3;
    public int foodCount = 0;
    public GameObject[] babylist;
    public bool full;

    private GameObject[] list;
    private int x = 0;
   
    //public List<GameObject> foodlist;


    // Use this for initialization
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        full = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        list = GameObject.FindGameObjectsWithTag("LightEmUp");
        foreach (GameObject item in list)
        {
            if (item.gameObject.layer == LayerMask.NameToLayer("Food")
                && Vector3.Distance(item.transform.position, this.transform.position) <= 7
                && !full )
            {
                
                babylist[x].GetComponent<BabytoFood>().food = item;
                babylist[x].GetComponent<BabytoFood>().caneat = true;
                x++;
                full = true;
            }
        }
        if (foodCount >= foodNeed)
        {
            gameScript.gameWin = true;
        }
    }
}
/*       int foodCount = 0;
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
*/
