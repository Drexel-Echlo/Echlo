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
    public Text traitDescription;
    public Text traitName;


    public static int maxTraits = 1;
    public static int traits = 0;
    public static int maxCarry;
    
    public static bool hasFoodMagnet;
    public static bool hasCompass;
    public static bool hasFatTissue;
    public static bool hasExpulsion;

    private void Start()
    {
        maxCarry = 3;
        buttonManager[0].SetTraitActive(hasFatTissue);
        buttonManager[1].SetTraitActive(hasFoodMagnet);
        buttonManager[2].SetTraitActive(hasCompass);
        buttonManager[3].SetTraitActive(hasExpulsion);

        maxTraits = Mathf.Max(0, GameController.level - 1);

        

        if (maxTraits > 0)
        {
            Time.timeScale = 0;
            traitMenu.SetActive(true);
            pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            if (!traitMenu.activeSelf)
            {
                GameController.Instance.Pause();
            }
        }

        if (traitCountText != null)
        {
            traitCountText.text = "Trait Points: " + (maxTraits - traits);
        }
        // For Debug
        if (Input.GetKeyDown(KeyCode.T))
        {
            maxTraits = 4;
            Time.timeScale = (Time.timeScale + 1) % 2;
            traitMenu.SetActive(!traitMenu.activeSelf);
        }
    }

    public void ToggleFoodMagnet()
    {
        if (maxTraits > traits && !hasFoodMagnet)
        {
            hasFoodMagnet = true;
            buttonManager[1].SetTraitActive(hasFoodMagnet);
            traits++;
            traitDescription.text = ("Causes nearby food to drift towards you");
            traitName.text = ("Filter Feeder:");
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
            traitDescription.text = ("Displays an arrow that points in the direction of your young");
            traitName.text = ("Inner Compass:");
        }
        else if (hasCompass)
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
            buttonManager[0].SetTraitActive(hasFatTissue);
            traits++;
            traitDescription.text = ("Adds 1 health for each food presently held");
            traitName.text = ("Fat Tissue:");
        }
        else if (hasFatTissue)
        {
            hasFatTissue = false;
            buttonManager[0].SetTraitActive(hasFatTissue);
            traits--;
        }
    }
    public void ToggleViolentExpulsion()
    {
        if (maxTraits > traits && !hasExpulsion)
        {
            hasExpulsion = true;
            buttonManager[3].SetTraitActive(hasExpulsion);
            traits++;
            traitDescription.text = ("Reguigitating food causes kickback away from direction vomited");
            traitName.text = ("Violent Expulsion:");
        }
        else if (hasExpulsion)
        {
            hasExpulsion = false;
            buttonManager[3].SetTraitActive(hasExpulsion);
            traits--;
        }
    }

    public void TraitScreen()
    {
        Time.timeScale = 1;
        traitMenu.SetActive(false);
    }
}
