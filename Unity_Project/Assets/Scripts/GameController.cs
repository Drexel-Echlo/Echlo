using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameObject mainPlayer;
    public static GameObject player;
    public GameObject pauseMenu;
    public GameObject traitMenu;
    public GameObject deadLight;
    public GameObject winLight;

    public Text gameovertext;
    public Text gamewintext;
    public bool gameOver;
    public bool gameWin;
    private bool restart;
    public static int level;
    public float yearsAlive; // Themeatic more than anything

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
        level = SceneManager.GetActiveScene().buildIndex;
        getMainPlayer();
        deadLight.SetActive(false);
        winLight.SetActive(false);
        Debug.Log(level);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameovertext.gameObject.SetActive(true);
            gameovertext.gameObject.GetComponent<Text>().text = " Your species is extinct" ;
            deadLight.SetActive(true);
            restart = true;
            Time.timeScale = 0;
            //Instantiate(deadLight, player.transform.position, Quaternion.identity);
            Destroy(player);

        }
        else if (gameWin)
        {
            TraitSystem.maxTraits++;
            gamewintext.gameObject.SetActive(true);
            yearsAlive += Mathf.Round(Time.timeSinceLevelLoad * 10 ) / 10;
            for (int i = 0; i < 1000; i++)
            {
                    gamewintext.gameObject.GetComponent<Text>().text = (Mathf.Round(Time.timeSinceLevelLoad * 10) / 10) + " Million Years Pass";
            }
            //gamewintext.gameObject.GetComponent<Text>().text = yearsAlive + " Millions Years Later";
            //winLight.SetActive(true);
            restart = true;
            Time.timeScale = 0;

        }

        if (restart && Input.anyKeyDown)
        {
            if (gameWin)
            {
                if (level >= SceneManager.sceneCountInBuildSettings - 1)
                {
                    level = 0;
                    SceneManager.LoadScene(level, LoadSceneMode.Single);
                    Debug.Log("StartScreen");
                }
                else 
                {
                    level++;
                    SceneManager.LoadScene(level, LoadSceneMode.Single);
                    Pause();
                    traitMenu.SetActive(!traitMenu.activeSelf);
                    pauseMenu.SetActive(!pauseMenu.activeSelf);
                    Debug.Log("else ");
                }
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Pause();
                traitMenu.SetActive(!traitMenu.activeSelf);
                pauseMenu.SetActive(!pauseMenu.activeSelf);
            }
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
