﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToHome : MonoBehaviour {

    public GameObject target;
    private Vector3 point;
    public GameObject player;
    private PlayerController moveScript;
    public GameObject pointer;
    // Use this for initialization
    void Start () {
        point = target.transform.position;
        moveScript = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!moveScript.isHome && !pointer.gameObject.activeSelf && TraitSystem.hasCompass)
        {
            pointer.gameObject.SetActive(true);
        }

        if (pointer.gameObject.activeSelf)
        {
            if (!TraitSystem.hasCompass || moveScript.isHome)
            {
                pointer.gameObject.SetActive(false);
            }
            else
            {
                float angle = Mathf.Atan2(point.x - transform.position.x, point.z - transform.position.z) * Mathf.Rad2Deg;

                this.transform.eulerAngles = new Vector3(0, angle, transform.rotation.y);
            }
        }
    }
}