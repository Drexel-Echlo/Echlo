using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownOverTime : MonoBehaviour {

    public Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (this.rb.velocity.x != 0 || this.rb.velocity.z != 0)
        {
            rb.velocity -= new Vector3(rb.velocity.x * Time.deltaTime / 1.5f, rb.velocity.y, rb.velocity.z * Time.deltaTime / 1.5f);
        }
        else
        {
            return;
        }
	}
}
