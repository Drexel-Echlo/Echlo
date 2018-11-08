using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameObject mainPlayer;
    public static GameObject player;
    public GameObject pauseMenu;

    public Text gameovertext;
    public Text gamewintext;
    public bool gameOver;
    public bool gameWin;
    private bool restart;
    public int level;

    public bool isPauseActive;

    protected static GameController _instance = null;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
                if (_instance == null)
                {
                    Debug.LogError("Instance of GameManager missing.");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (GameController.Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        gameOver = false;
        restart = false;
        Time.timeScale = 1f;
        level = SceneManager.GetActiveScene().buildIndex;
        getMainPlayer();
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
            if (gameWin && level == SceneManager.sceneCountInBuildSettings)
            {
                level = 0;
                SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
            }
            else if (gameWin)
            {
                level++;
                SceneManager.LoadScene(level, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPauseActive = !isPauseActive;
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
        Time.timeScale = (Time.timeScale + 1) % 2;
    }

    public static GameObject getMainPlayer()
    {
        if (mainPlayer != null)
        {
            return mainPlayer;
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            PlayerController playerController = p.GetComponent<PlayerController>();
            if (playerController != null)
            {
                mainPlayer = p;
            } else
            {
                player = p;
            }
        }
        return mainPlayer;
    }
}
