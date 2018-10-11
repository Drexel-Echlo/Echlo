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
        else if (player.vertical > 0 && player.horizontal == 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(90, transform.rotation.y, 90);
        }
        else if (player.vertical < 0 && player.horizontal == 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(90, transform.rotation.y, 270);
        }
        else if (player.horizontal > 0 && player.vertical == 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(90, transform.rotation.y, 0);
        }
        else if (player.horizontal < 0 && player.vertical == 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(90, transform.rotation.y, 180);
        }
        else if (player.horizontal > 0 && player.vertical > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(90, transform.rotation.y, 45);
        }
        else if (player.horizontal < 0 && player.vertical > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(90, transform.rotation.y, 135);
        }
        else if (player.horizontal < 0 && player.vertical < 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(90, transform.rotation.y, 225);
        }
        else if (player.horizontal > 0 && player.vertical < 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(90, transform.rotation.y, 315);
        }
        else
        {
            return;
        }
    }
}
