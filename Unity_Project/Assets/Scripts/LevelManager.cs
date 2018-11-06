using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public Button play;
    public Button exit;

    private void Start()
    {
        play.onClick.AddListener(LoadLevel);
        exit.onClick.AddListener(ExitGame);
    }
    private void LoadLevel()
    {
        SceneManager.LoadScene("Level_01", LoadSceneMode.Single);
    }
    private void ExitGame()
    {
        Application.Quit();
    }



}
