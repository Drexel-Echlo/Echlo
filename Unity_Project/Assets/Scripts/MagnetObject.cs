using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetObject : MonoBehaviour {

    public GameObject player;

    public float drawSpeed;

    private bool isInMagnet = false;
    
    // Update is called once per frame
    void Update ()
    {
        Vector3 playerPos = player.transform.position;
		if (isInMagnet)
        {
            transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
            transform.Translate(Vector3.forward * drawSpeed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FoodMagnet")
        {
            isInMagnet = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FoodMagnet")
        {
            isInMagnet = false;
        }
    }
}
