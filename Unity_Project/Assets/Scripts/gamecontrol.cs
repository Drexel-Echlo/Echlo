using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamecontrol : MonoBehaviour {

	//public GameObject food;
	//public PlayerController player;
	//public GameObject enemy;

	public bool gameOver;
	private bool restart;


	// Use this for initialization
	void Start () {
		gameOver = false;
		restart = false;
		Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {


		if (gameOver) {
			Debug.Log (gameOver);
			restart = true;
			Time.timeScale = 0;
		}


		if (restart){
			if (Input.GetKeyDown (KeyCode.R)){
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
}
