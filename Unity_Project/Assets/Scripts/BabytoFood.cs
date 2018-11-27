using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabytoFood : MonoBehaviour {

    public Vector3 stats;
    public GameObject endpoint;
    public float speed;
    private GameObject[] list;
    private bool getfood;
    private bool full;
    //public int totalBabyFood;
    protected CheckFoodCount gameScript;

    // Use this for initialization
    void Start () {
        stats = this.transform.position;
        gameScript = GameObject.Find("Home").GetComponent<CheckFoodCount>();
        getfood = false;
        full = false;
    }


    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Food(Clone)")
        {
            //Debug.Log("find it");
            Destroy(other.gameObject);
            gameScript.foodCount++;
            Debug.Log(gameScript.foodCount);
            getfood = true;
            full = true;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Food(Clone)")
        {
            //Debug.Log("find it");
            gameScript.foodCount++;
            Destroy(other.gameObject);
            Debug.Log(gameScript.foodCount);
            getfood = true;
            full = true;
        }
    }


    // Update is called once per frame
    void Update ()
    {
        //int foodCount = 0;
        float step = speed * Time.deltaTime;
        list = GameObject.FindGameObjectsWithTag("LightEmUp");

        foreach (GameObject item in list)
        {
            if (item.gameObject.layer == LayerMask.NameToLayer("Food") 
                && Vector3.Distance(item.transform.position, this.transform.position) <= 7
                && !full)
            {
                transform.LookAt(item.transform);
                transform.position = Vector3.MoveTowards(transform.position, item.transform.position, step);
                //foodlist.Add(item);
            }
        }

        if (getfood)
        {
            if (Vector3.Distance(endpoint.transform.position, this.transform.position) <= 3)
            {
                
                getfood = false;
            }
            transform.LookAt(endpoint.transform);
            transform.position = Vector3.MoveTowards(transform.position, endpoint.transform.position, step);
        }
    }

}
