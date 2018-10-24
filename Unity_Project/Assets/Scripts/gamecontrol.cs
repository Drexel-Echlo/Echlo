using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class gamecontrol : MonoBehaviour {

    public GameObject player;
    //public PlayerController player;
    //public GameObject enemy;

    public Text gameovertext;
    public Text gamewintext;
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
            gameovertext.gameObject.SetActive(true);
			restart = true;
            Time.timeScale = 0;
            Destroy(player);
        }

		if (restart && Input.anyKeyDown){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
