using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetObject : MonoBehaviour {

    public float drawSpeed;
    protected GameObject player;
    protected PlayerController playerController;
    public Transform home;
    private bool isInMagnet = false;
    protected bool isHome = false; 

    private void Start()
    {
        home = GameObject.FindGameObjectWithTag("Home").transform;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            playerController = p.GetComponent<PlayerController>();
            if (playerController != null)
            {
                player = p;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if (isInMagnet && TraitSystem.hasFoodMagnet && playerController.carryCount < TraitSystem.maxCarry && !playerController.isHome && !isHome)
        {
            Vector3 playerPos = player.transform.position;
            transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
            transform.Translate(Vector3.forward * drawSpeed * Time.deltaTime);
        }
        if (!isHome && Vector3.Distance(transform.position, home.position) <= 7)
        {
            isHome = true;
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
