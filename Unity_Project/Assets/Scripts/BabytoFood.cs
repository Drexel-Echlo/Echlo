using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabytoFood : MonoBehaviour {

    public Vector3 stats;
    public GameObject endpoint;
    public float speed;
    private GameObject[] list;

    // Use this for initialization
    void Start () {
        stats = this.transform.position;
	}


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "Food(Clone)")
        {
            Debug.Log("find it");
            Destroy(other.gameObject);
            Moveout();
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
            if (item.gameObject.layer == LayerMask.NameToLayer("Food") && Vector3.Distance(item.transform.position, this.transform.position) <= 7)
            {
                transform.LookAt(item.transform);
                transform.position = Vector3.MoveTowards(transform.position, item.transform.position, step);
                //foodlist.Add(item);


            }
        }
    }

    private void Moveout()
    {
        float step = speed * Time.deltaTime;

        transform.LookAt(endpoint.transform);
        transform.position = Vector3.MoveTowards(transform.position, endpoint.transform.position, step);
    }
}
