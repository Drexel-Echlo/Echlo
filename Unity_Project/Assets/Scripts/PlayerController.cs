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
    public bool isHome;

    // Use this for initialization
    void Start () {
		playerScript = player.GetComponent<PlayerRotator>();
    }

	// Update is called once per frame
	void FixedUpdate () {
		
		float step = speed * Time.deltaTime;

        if (Input.GetKey("w"))
        {
			transform.position = Vector3.MoveTowards (transform.position, playerScript.point, step);
		}
        else if (Input.GetKey("s"))
        {
			transform.position = Vector3.MoveTowards (transform.position, playerScript.point, -step/4);
		}

        if (allowLight && (Input.GetKey("w") || Input.GetKey("s")))
        {
            StartCoroutine(LightTrail(lightFrequency));
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
    }

    private void OnCollisionEnter (Collision other)
    {
        if (!isCarryingFood && other.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            isCarryingFood = true;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider c)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && isCarryingFood && c.gameObject.tag == "Home")
        {
            isCarryingFood = false;
            Instantiate(foodDrop, mouth.position, Quaternion.identity);
            
            // The Home Tag is good, because vomiting to baby fish while home will is 1/3 of completing a level.
            //I was also thinking you could vommit when ever, but not add to level score etc.
            /*if (Input.GetKeyDown(KeyCode.Mouse1) && isCarryingFood && c.gameObject.tag == "Home")
                {
                isCarryingFood = false;
                Instantiate(vomitFood, mouth.position, Quaternion.identity);
                LevelScore++;
                }
            else if (Input.GetKeyDown(KeyCode.Space) && isCarryingFood)
                {
                isCarryingFood = false;
                Instantiate(vomitFood, mouth.position, Quaternion.identity);
                }*/
        }
        else if (!isCarryingFood && c.gameObject.tag == "FoodPickupRadius")
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                isCarryingFood = true;
                Destroy(c.transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Home")
        {
            isHome = true;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Home")
        {
            isHome = false;
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
