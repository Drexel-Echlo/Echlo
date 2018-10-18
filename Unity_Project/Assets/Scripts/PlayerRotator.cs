using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour {
	/*
    public PlayerController player;
    
	// Update is called once per frame
	void Update () {

        if (player.vertical == 0 && player.horizontal == 0)
        {
            return;
        }
        // UP
        else if (player.vertical > 0 && player.horizontal == 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        // DOWN
        else if (player.vertical < 0 && player.horizontal == 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        // RIGHT
        else if (player.horizontal > 0 && player.vertical == 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        // LEFT
        else if (player.horizontal < 0 && player.vertical == 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
        }
        // UP RIGHT
        else if (player.horizontal > 0 && player.vertical > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 45, 0);
        }
        // UP LEFT
        else if (player.horizontal < 0 && player.vertical > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 315, 0);
        }
        // DOWN LEFT
        else if (player.horizontal < 0 && player.vertical < 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 225, 0);
        }
        // DOWN RIGHT
        else if (player.horizontal > 0 && player.vertical < 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 135, 0);
        }
        else
        {
            return;
        }
    }*/
	private Camera main;

	void Start () {
		main = FindObjectOfType<Camera> ();
	}

	void Update () {
		Ray cameraRay = main.ScreenPointToRay (Input.mousePosition);
		Plane gound = new Plane (Vector3.up, Vector3.zero);
		float rayLength;

		if (gound.Raycast (cameraRay, out rayLength)) {
			Vector3 point = cameraRay.GetPoint (rayLength);

			//Debug.DrawLine (cameraRay.origin, point, Color.blue);

			//Vector3 vec = new Vector3 (point.x - transform.position.x, 0, point.z - transform.position.z);
			float angle = Mathf.Atan2 (point.x - transform.position.x, point.z - transform.position.z) * Mathf.Rad2Deg;

			Debug.Log (angle);

			this.gameObject.transform.eulerAngles = new Vector3 (90, transform.rotation.y, -angle + 90);
		}
	}
}
