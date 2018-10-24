using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public GameObject player;
    public GameObject gameManager;
    private PlayerRotator rotationScript;
    private gamecontrol gameScript;

    public TraitSystem traits;

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
		rotationScript = player.GetComponent<PlayerRotator>();
        gameScript = gameManager.GetComponent<gamecontrol>();
        traits = GameObject.Find("TraitManager").GetComponent<TraitSystem>();
    }

	void FixedUpdate () {
		
		float step = speed * Time.deltaTime;

        if (Input.GetKey("w"))
        {
			transform.position = Vector3.MoveTowards(transform.position, rotationScript.point, step);
		}
        else if (Input.GetKey("s"))
        {
			transform.position = Vector3.MoveTowards (transform.position, rotationScript.point, -step/4);
		}

        if (allowLight && (Input.GetKey("w") || Input.GetKey("s")))
        {
            StartCoroutine(LightTrail(lightFrequency));
        }

        // Handle Food Display on character
        if ((carryCount == 3 && carryingFoodClone[2] == null) ||
            (carryCount == 2 && (carryingFoodClone[1] == null || (carryingFoodClone[1] != null && carryingFoodClone[2] != null))) ||
            (carryCount == 1 && (carryingFoodClone[0] == null || (carryingFoodClone[0] != null && carryingFoodClone[1] != null))) ||
            (carryCount == 0))
        {
            displayFood(carryCount);
        }
    }

    protected void displayFood(int count)
    {
        for (int i = 0; i < 3; i++)
        {
            if (carryingFoodClone[i] != null)
            {
                Destroy(carryingFoodClone[i]);
            }
            carryingFoodClone[i] = null;
        }

        if (count > 0)
        {
            carryingFoodClone[0] = Instantiate(carryingFood, new Vector3(transform.position.x - 1.2f, 2, transform.position.z), Quaternion.identity);
        }
   
        if (count > 1)
        {
            carryingFoodClone[1] = Instantiate(carryingFood, new Vector3(transform.position.x - .7f, 2, transform.position.z - .2f), Quaternion.identity);
        }

        if (count > 2)
        {
            carryingFoodClone[2] = Instantiate(carryingFood, new Vector3(transform.position.x - .7f, 2, transform.position.z + .2f), Quaternion.identity);
        }

        for (int i = 0; i < count; i++)
        {
            carryingFoodClone[i].transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionEnter (Collision other)
    {
        /*if (carryCount < traits.maxCarry && other.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            carryCount++;
            Destroy(other.gameObject);
        }*/

        //check if an enemy touches the player
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            gameScript.gameOver = true;
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
        else if (Input.GetKeyDown(KeyCode.Mouse1) && carryCount < traits.maxCarry && c.gameObject.tag == "FoodPickupRadius")
        {
            carryCount++;
            Destroy(c.transform.parent.gameObject);
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
