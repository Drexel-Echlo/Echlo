using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitSystem : MonoBehaviour {
    
    public Button[] button;

    public ButtonManager[] buttonManager;
    public static int maxCarry;

    public static bool hasDigestiveTrack;
    public static bool hasFoodMagnet;
    public static bool hasCompass;

    private void Start()
    {
        maxCarry = (hasDigestiveTrack ? 3 : 1);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!GameController.Instance.pauseMenu.gameObject.activeSelf || button[0].gameObject.activeSelf == true)
            {
                GameController.Instance.Pause();
                button[0].gameObject.SetActive(!button[0].gameObject.activeSelf);
                button[1].gameObject.SetActive(!button[1].gameObject.activeSelf);
                button[2].gameObject.SetActive(!button[2].gameObject.activeSelf);
            }
            else if (GameController.Instance.pauseMenu.gameObject.activeSelf)
            {
                button[0].gameObject.SetActive(!button[0].gameObject.activeSelf);
                button[1].gameObject.SetActive(!button[1].gameObject.activeSelf);
                button[2].gameObject.SetActive(!button[2].gameObject.activeSelf);
            }
        }

        if (!GameController.Instance.isPauseActive)
        {
            button[0].gameObject.SetActive(false);
            button[1].gameObject.SetActive(false);
            button[2].gameObject.SetActive(false);
        }
    }

    public void ToggleDigestiveTrack()
    {
        hasDigestiveTrack = !hasDigestiveTrack;
        buttonManager[0].SetTraitActive(hasDigestiveTrack);
        maxCarry = (hasDigestiveTrack ? 3 : 1);
    }

    public void ToggleFoodMagnet()
    {
        hasFoodMagnet = !hasFoodMagnet;
        buttonManager[1].SetTraitActive(hasFoodMagnet);
    }
    public void ToggleCompass()
    {
        hasCompass = !hasCompass;
        buttonManager[2].SetTraitActive(hasCompass);
    }
}
