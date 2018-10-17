using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody rb;

    public Transform mouth;

    public float horizontal;
    public float vertical;
    public float maxSpeed;

    public SpriteRenderer spriteR;
    public Sprite spriteNormal;
    public Sprite spriteFull;
    public GameObject foodDrop;

    public bool isCarryingFood;

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
            spriteR.sprite = spriteFull;
        }
        else
        {
            spriteR.sprite = spriteNormal;
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
        if (Input.GetKeyDown(KeyCode.Space) && isCarryingFood)
        {
            isCarryingFood = false;
            Instantiate(foodDrop, mouth.position, Quaternion.identity);
        }
        else
        {
            return;
        }
    }
}
