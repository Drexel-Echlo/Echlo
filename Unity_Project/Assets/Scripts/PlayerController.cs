using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
	public GameObject player;
	private PlayerRotator playerScript;

    public TraitSystem trait;

    public Transform mouth;
    public Transform lightSpawn;

	public float speed;
    public float lightFrequency = 1f;

    public int carryCount;

    public GameObject foodDrop;
    public GameObject carryingFood;
    public GameObject[] carryingFoodClone = new GameObject[3];

    private bool allowLight = true;
    public GameObject movingLight;
    public GameObject movingLightClone;
    
    public bool isFoodCreated;
    public bool isHome;

    // Use this for initialization
    void Start () {
		playerScript = player.GetComponent<PlayerRotator>();
        trait = GameObject.Find("TraitManager").GetComponent<TraitSystem>();
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

        /* if (carryCount != 0)
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
         }*/

        if (carryCount == 3 && carryingFoodClone[2] == null)
        {
            for (int i = 0; i < 3; i++)
            {
                if (carryingFoodClone[i] != null)
                {
                    Destroy(carryingFoodClone[i]);
                }
                carryingFoodClone[i] = null;
            }
            carryingFoodClone[0] = Instantiate(carryingFood, new Vector3(transform.position.x - 1.2f, 2, transform.position.z), Quaternion.identity) as GameObject;
            carryingFoodClone[0].transform.parent = gameObject.transform;

            carryingFoodClone[1] = Instantiate(carryingFood, new Vector3(transform.position.x - .7f, 2, transform.position.z - .2f), Quaternion.identity) as GameObject;
            carryingFoodClone[1].transform.parent = gameObject.transform;

            carryingFoodClone[2] = Instantiate(carryingFood, new Vector3(transform.position.x - .7f, 2, transform.position.z + .2f), Quaternion.identity) as GameObject;
            carryingFoodClone[2].transform.parent = gameObject.transform;
        }
        if (carryCount == 2 && (carryingFoodClone[1] == null || (carryingFoodClone[1] != null && carryingFoodClone[2] != null)))
        {
            for (int i = 0; i < 3; i++)
            {
                if (carryingFoodClone[i] != null)
                {
                    Destroy(carryingFoodClone[i]);
                }
                carryingFoodClone[i] = null;
            }
            carryingFoodClone[0] = Instantiate(carryingFood, new Vector3(transform.position.x - 1.2f, 2, transform.position.z), Quaternion.identity) as GameObject;
            carryingFoodClone[0].transform.parent = gameObject.transform;

            carryingFoodClone[1] = Instantiate(carryingFood, new Vector3(transform.position.x - .7f, 2, transform.position.z - .2f), Quaternion.identity) as GameObject;
            carryingFoodClone[1].transform.parent = gameObject.transform;
        }
        if (carryCount == 1 && (carryingFoodClone[0] == null || (carryingFoodClone[0] != null && carryingFoodClone[1] != null)))
        {
            for (int i = 0; i < 3; i++)
            {
                if (carryingFoodClone[i] != null)
                {
                    Destroy(carryingFoodClone[i]);
                }
                carryingFoodClone[i] = null;
            }
            carryingFoodClone[0] = Instantiate(carryingFood, new Vector3(transform.position.x - 1.2f, 2, transform.position.z), Quaternion.identity) as GameObject;
            carryingFoodClone[0].transform.parent = gameObject.transform;
        }
        if (carryCount == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (carryingFoodClone[i] != null)
                {
                    Destroy(carryingFoodClone[i]);
                }
                carryingFoodClone[i] = null;
            }
        }
    }

    private void OnCollisionEnter (Collision other)
    {
        if (carryCount < trait.maxCarry && other.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            carryCount++;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider c)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && carryCount != 0 && c.gameObject.tag == "Home")
        {
            carryCount--;
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
        else if (carryCount < trait.maxCarry && c.gameObject.tag == "FoodPickupRadius")
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                carryCount++;
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
