using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitSystem : MonoBehaviour {
    
    public Button[] button;

    public ButtonManager[] buttonManager;

    public  GameObject magnet;

    public int maxCarry;

    public static bool hasDigestiveTrack;
    public static bool hasFoodMagnet;
   	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            button[0].gameObject.SetActive(!button[0].gameObject.activeSelf);
            button[1].gameObject.SetActive(!button[1].gameObject.activeSelf);
            Time.timeScale = (Time.timeScale + 1)%2;
        }

        if (hasDigestiveTrack)
        {
            maxCarry = 3;
        }
        else
        {
            maxCarry = 1;
        }
        if (magnet != null)
        {
            if (hasFoodMagnet)
            {
                magnet.gameObject.SetActive(true);
            }
            else
            {
                magnet.gameObject.SetActive(false);
            }
        }
    }

    public void ToggleDigestiveTrack()
    {
        hasDigestiveTrack = !hasDigestiveTrack;
        buttonManager[0].SetTraitActive(hasDigestiveTrack);
    }

    public void ToggleFoodMagnet()
    {
        hasFoodMagnet = !hasFoodMagnet;
        buttonManager[1].SetTraitActive(hasFoodMagnet);
    }
}
