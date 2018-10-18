using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour {

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
    }
}
