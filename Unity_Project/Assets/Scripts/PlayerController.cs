using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public GameObject player;
    private PlayerRotator rotationScript;
    private GameController gameScript;
    private ShooterController shooter;

    public Transform mouth;
    public Transform lightSpawn;
    public Transform[] foodPoints;

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
        gameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        shooter = player.GetComponent<ShooterController>();
    }

	void FixedUpdate () {
		
		float step = speed * Time.deltaTime;

        if (Input.GetKey("w")) // Move Forward
        {
			transform.position = Vector3.MoveTowards(transform.position, rotationScript.point, step);
            if (allowLight) // Light Trail
            {
                StartCoroutine(LightTrail(lightFrequency));
            }
        }
        else if (Input.GetKey("s")) // Move Back
        {
			transform.position = Vector3.MoveTowards (transform.position, rotationScript.point, -step/4);
		}

        if (Input.GetKeyDown(KeyCode.Mouse0)) // Sonar
        {
            shooter.shootWave(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && carryCount != 0 && !isHome) // Spit Food
        {
            shooter.spitFood();
            carryCount--;
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
            carryingFoodClone[0] = Instantiate(carryingFood, foodPoints[0].position, Quaternion.identity);
        }
   
        if (count > 1)
        {
            carryingFoodClone[1] = Instantiate(carryingFood, foodPoints[1].position, Quaternion.identity);
        }

        if (count > 2)
        {
            carryingFoodClone[2] = Instantiate(carryingFood, foodPoints[2].position, Quaternion.identity);
        }

        for (int i = 0; i < count; i++)
        {
            carryingFoodClone[i].transform.parent = player.transform;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (carryCount < TraitSystem.maxCarry && other.gameObject.layer == LayerMask.NameToLayer("Food") && !isHome)
        {
            Destroy(other.gameObject);
            carryCount++;
        }

        // Check if an enemy touches the player
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Stalker"))
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Home")
        {
            isHome = true;
        }
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Light")))
        {
            GameObject[] stalkers = GameObject.FindGameObjectsWithTag("LightEmUp");
            foreach (GameObject stalker in stalkers)
            {
                StalkerConroller stalkerController = stalker.GetComponent<StalkerConroller>();
                if (stalkerController != null)
                {
                    stalkerController.setTarget(transform.position);
                }

            }
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
        movingLightClone.AddComponent(typeof(PositionHolder));
        movingLightClone.GetComponent<PositionHolder>().position = transform.position;
        GameObject.Destroy(movingLightClone, 2.5f);

        WaitForSeconds delay = new WaitForSeconds(frequency);
        yield return delay;

        allowLight = true;
    }
}
