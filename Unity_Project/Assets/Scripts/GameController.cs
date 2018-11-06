using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject player;
    //public PlayerController player;
    //public GameObject enemy;

    public Text gameovertext;
    public Text gamewintext;
    public bool gameOver;
    public bool gameWin;
    private bool restart;
    public int levelNum;

    // Use this for initialization
    void Start()
    {
        gameOver = false;
        restart = false;
        Time.timeScale = 1f;
        Scene level = SceneManager.GetActiveScene();
        levelNum = level.buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameovertext.gameObject.SetActive(true);
            restart = true;
            Time.timeScale = 0;
            Destroy(player);
        }
        else if (gameWin)
        {
            gamewintext.gameObject.SetActive(true);
            restart = true;
            Time.timeScale = 0;
        }

        if (restart && Input.anyKeyDown)
        {
            if (gameWin && levelNum == SceneManager.sceneCountInBuildSettings)
            {
                levelNum = 0;
                SceneManager.LoadScene(levelNum, LoadSceneMode.Single);
            }
            else if (gameWin)
            {
                levelNum++;
                SceneManager.LoadScene(levelNum, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
