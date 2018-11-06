using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public Button play;

    private void Start()
    {
        play.onClick.AddListener(LoadLevel);
    }
    private void LoadLevel()
    {
        SceneManager.LoadScene("Level_01", LoadSceneMode.Single);
    }


}
