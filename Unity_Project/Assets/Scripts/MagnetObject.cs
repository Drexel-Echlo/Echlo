using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetObject : MonoBehaviour {

    public float drawSpeed;
    protected GameObject player;
    protected PlayerController playerController;
    public Transform home;
    private bool isInMagnet = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        home = GameObject.FindGameObjectWithTag("Home").transform;
    }

    // Update is called once per frame
    void Update ()
    {
		if (isInMagnet && TraitSystem.hasFoodMagnet && playerController.carryCount < TraitSystem.maxCarry && !playerController.isHome && !(Vector3.Distance(transform.position, home.position) <= 7))
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
