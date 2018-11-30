using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public GameObject player;
    private PlayerRotator rotationScript;
    private GameController gameScript;
    private ShooterController shooter;
    private bool canPickUpFood = true;
    
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

    public Animator anim;

    public bool isFoodCreated;
    public bool isHome;
    protected ArrayList stalkers;

    private float chargeTime;

    // Use this for initialization
    void Start()
    {
        rotationScript = player.GetComponent<PlayerRotator>();
        gameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        shooter = player.GetComponent<ShooterController>();
        stalkers = new ArrayList();
        GameObject[] lightemUps = GameObject.FindGameObjectsWithTag("LightEmUp");
        foreach (GameObject stalker in lightemUps)
        {
            StalkerConroller stalkerController = stalker.GetComponent<StalkerConroller>();
            if (stalkerController != null)
            {
                stalkers.Add(stalkerController);
            }
        }
    }


	void FixedUpdate () {
		
		float step = speed * Time.deltaTime;

        if (Input.GetKey("w")) // Move Forward
        {
            transform.position += step * player.transform.forward;

            anim.SetBool("isMoving", true);
            anim.SetFloat("animationSpeed", 1);

            if (allowLight) // Light Trail
            {
                StartCoroutine(LightTrail(lightFrequency));
            }
        }        
        else if (Input.GetKey("s")) // Move Back
        {
			transform.position -= (step / 4) * player.transform.forward;

            anim.SetBool("isMoving", true);
            anim.SetFloat("animationSpeed", .25f);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (Input.GetKey(KeyCode.Mouse0)) // Sonar
        {
            if (TraitSystem.hasVocalCords)
            {
                chargeTime += Time.deltaTime;
            }

            shooter.shootWave(transform.position);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (chargeTime >= 2 && TraitSystem.hasVocalCords)
            {
                chargeTime = 0;
                shooter.shootPowerWave(transform.position);
            }
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && carryCount != 0) // Spit Food
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            if (TraitSystem.hasExpulsion)
                rb.AddForce(-player.transform.forward * 400);

            if (shooter.spitFood())
            {
                carryCount--;
            }
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
        if (canPickUpFood)
        {
            canPickUpFood = false;
            if (carryCount < TraitSystem.maxCarry && other.gameObject.layer == LayerMask.NameToLayer("Food"))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                Destroy(other.gameObject);
                carryCount++;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(100);
                    canPickUpFood = true;
                }).Start();
            } else
            {
                canPickUpFood = true;
            }
        }


        // Check if an enemy touches the player
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Stalker"))
        {
            if (TraitSystem.hasFatTissue && carryCount > 0)
            {
                shooter.spitFood();
                carryCount--;
            }
            else
            {
                gameScript.gameOver = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Home")
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
        if (c.gameObject.layer.Equals(LayerMask.NameToLayer("Light")))
        {
            foreach (StalkerConroller stalkerController in stalkers)
            {
                stalkerController.setTarget(transform.position);
            }
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
