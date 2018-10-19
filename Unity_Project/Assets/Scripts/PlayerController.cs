using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
	public GameObject player;
	private PlayerRotator playerScript;

    public Transform mouth;
    public Transform lightSpawn;

	public float speed;
    public float lightFrequency = 1f;
    
    public GameObject foodDrop;
    public GameObject carryingFood;
    public GameObject carryingFoodClone;

    private bool allowLight = true;
    public GameObject movingLight;
    public GameObject movingLightClone;

    public bool isCarryingFood;
    public bool isFoodCreated;

    // Use this for initialization
    void Start () {
		playerScript = player.GetComponent<PlayerRotator>();
    }

	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		if (Input.GetKey("w")) {
			transform.position = Vector3.MoveTowards (transform.position, playerScript.point, step);
		} else if (Input.GetKey("s")) {
			transform.position = Vector3.MoveTowards (transform.position, playerScript.point, -step/4);
		}

        if (isCarryingFood)
        {
            if (!isFoodCreated)
            {
                carryingFoodClone = Instantiate(carryingFood, new Vector3(transform.position.x - 1.2f, 2, transform.position.z), Quaternion.identity) as GameObject;
                carryingFoodClone.transform.parent = gameObject.transform;
                isFoodCreated = true;
            }
        }
        else
        {
            Destroy(carryingFoodClone);
            isFoodCreated = false;
        }

        if (allowLight && (Input.GetKey("w") || Input.GetKey("s")))
        {
            StartCoroutine(LightTrail(lightFrequency));
        }
    }

    private void OnCollisionEnter (Collision other)
    {
        if (!isCarryingFood && other.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            isCarryingFood = true;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCarryingFood && other.gameObject.tag == "Home")
        {
            isCarryingFood = false;
            Instantiate(foodDrop, mouth.position, Quaternion.identity);
        }
    }

    IEnumerator LightTrail(float frequency)
    {
        allowLight = false;

        movingLightClone = Instantiate(movingLight, lightSpawn.position, transform.rotation);
        GameObject.Destroy(movingLightClone, 3);

        WaitForSeconds delay = new WaitForSeconds(frequency);
        yield return delay;

        allowLight = true;
    }
}
