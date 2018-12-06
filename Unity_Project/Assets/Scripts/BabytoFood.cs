using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabytoFood : MonoBehaviour {

    //public Vector3 stats;
    public GameObject endpoint;
    public GameObject food;
    public float speed;
    
    private bool getfood;
    private bool full;
    
    protected CheckFoodCount gameScript;

    // Use this for initialization
    void Start () {
        //stats = this.transform.position;
        gameScript = GameObject.Find("Home").GetComponent<CheckFoodCount>();
        getfood = false;
        food = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Food(Clone)")
        {
            //Debug.Log("find it");
            gameScript.foodCount++;
            Destroy(other.gameObject);
            food = null;
            Debug.Log(gameScript.foodCount);
            getfood = true;
            gameScript.full = false;
        }
    }


    // Update is called once per frame
    void Update ()
    {
        float step = speed * Time.deltaTime;
        if (food != null)
        {
            transform.LookAt(food.transform);
            transform.position = Vector3.MoveTowards(transform.position, food.transform.position, step);
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
