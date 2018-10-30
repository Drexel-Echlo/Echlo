using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetObject : MonoBehaviour {

    public float drawSpeed;
    protected GameObject player;
    private bool isInMagnet = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  
    }
    // Update is called once per frame
    void Update ()
    {
		if (isInMagnet && TraitSystem.hasFoodMagnet)
        {
            Vector3 playerPos = player.transform.position;
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
