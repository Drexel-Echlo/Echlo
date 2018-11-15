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
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!GameController.Instance.pauseMenu.gameObject.activeSelf || button[0].gameObject.activeSelf == true)
            {
                GameController.Instance.Pause();
                traitMenu.SetActive(false);
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
        traitMenu.SetActive(!traitMenu.activeSelf);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
