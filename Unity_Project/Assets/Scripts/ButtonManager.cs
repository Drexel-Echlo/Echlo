using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Button button;

    public TraitSystem traitSystem;

    public string trait;
    public string enabledText;
    public string disabledText;

    private void Start()
    {
        button.GetComponentInChildren<Text>().text = trait + ": " + disabledText;
    }

    // Update is called once per frame
    public void SetTraitActive(bool isTraitEnabled)
    {
        if (isTraitEnabled)
        {        
            button.GetComponentInChildren<Text>().text = trait + ": " + enabledText;
        }
        else
        {
            button.GetComponentInChildren<Text>().text = trait + ": " + disabledText;
        }
    }
}
