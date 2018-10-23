using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitSystem : MonoBehaviour {
    
    public Button button;

    public ButtonManager buttonManager;

    public int maxCarry;

    public bool hasDigestiveTrack;
   	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            button.gameObject.SetActive(!button.gameObject.activeSelf);
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
    }

    public void ToggleDigestiveTrack()
    {
        hasDigestiveTrack = !hasDigestiveTrack;
        buttonManager.SetTraitActive(hasDigestiveTrack);
    }
}
