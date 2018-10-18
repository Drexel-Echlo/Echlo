using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody rb;

    public Transform mouth;
    public Transform lightSpawn;

    public float horizontal;
    public float vertical;
    public float maxSpeed;
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
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(horizontal * maxSpeed, rb.velocity.y, vertical * maxSpeed);

        if (isCarryingFood)
        {
            if (!isFoodCreated)
            {
                carryingFoodClone = Instantiate(carryingFood, new Vector3(transform.position.x - 1.2f, 2, transform.position.z), Quaternion.identity) as GameObject;
                carryingFoodClone.transform.parent = gameObject.transform;
                isFoodCreated = true;
            }
            else
            {
                return;
            }
        }
        else
        {
            Destroy(carryingFoodClone);
            isFoodCreated = false;
        }

        if (allowLight)
        {
            if (horizontal != 0 || vertical != 0)
            {
                StartCoroutine(LightTrail(lightFrequency));
            }
        }
    }

    private void OnCollisionEnter (Collision other)
    {
        if (!isCarryingFood && other.gameObject.layer == 11)
        {
            isCarryingFood = true;
            Destroy(other.gameObject);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerStay(Collider home)
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCarryingFood && home.gameObject.tag == "Home")
        {
            isCarryingFood = false;
            Instantiate(foodDrop, mouth.position, Quaternion.identity);
        }
        else
        {
            return;
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
