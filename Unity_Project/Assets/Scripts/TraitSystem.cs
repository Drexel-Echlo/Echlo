using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitSystem : MonoBehaviour {
    
    public Button[] button;

    public ButtonManager[] buttonManager;
    public Text traitCountText;
    public GameObject traitMenu;
    public GameObject pauseMenu;

    public static int maxCarry;
    
    public static bool hasFoodMagnet;
    public static bool hasCompass;
    public static bool hasFatTissue;
    public static int maxTraits = 1;
    public static int traits = 0;

    private void Start()
    {
        maxCarry = 3;
        buttonManager[0].SetTraitActive(hasFatTissue);
        buttonManager[1].SetTraitActive(hasFoodMagnet);
        buttonManager[2].SetTraitActive(hasCompass);
        maxTraits = Mathf.Max(0,GameController.level - 1);
        if (maxTraits > 0)
        {
            Time.timeScale = 0;
            traitMenu.SetActive(true);
            pauseMenu.SetActive(false);
        } else
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!GameController.Instance.pauseMenu.gameObject.activeSelf && !traitMenu.activeSelf)
            {
                print("Pause");
                GameController.Instance.Pause();
            }
        }

        if (traitCountText != null)
        {
            traitCountText.text = "Trait Points: " + (maxTraits - traits);
        }
    }

    public void ToggleFoodMagnet()
    {
        if (maxTraits > traits && !hasFoodMagnet)
        {
            hasFoodMagnet = true;
            buttonManager[1].SetTraitActive(hasFoodMagnet);
            traits++;
        }
        else if (hasFoodMagnet)
        {
            hasFoodMagnet = false;
            buttonManager[1].SetTraitActive(hasFoodMagnet);
            traits--;
        }
    }
    public void ToggleCompass()
    {
        if (maxTraits > traits && !hasCompass)
        {
            hasCompass = true;
            buttonManager[2].SetTraitActive(hasCompass);
            traits++;
        } else if (hasCompass)
        {
            hasCompass = false;
            buttonManager[2].SetTraitActive(hasCompass);
            traits--;
        }
    }
    public void ToggleFatTissue()
    {
        if (maxTraits > traits && !hasFatTissue)
        {
            hasFatTissue = true;
            buttonManager[1].SetTraitActive(hasFatTissue);
            traits++;
        }
        else if (hasFatTissue)
        {
            hasFatTissue = false;
            buttonManager[1].SetTraitActive(hasFatTissue);
            traits--;
        }
    }

    public void TraitScreen()
    {
        Time.timeScale = 1;
        traitMenu.SetActive(false);
    }
}
