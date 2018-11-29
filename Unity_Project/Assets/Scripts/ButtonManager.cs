using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Button button;

    public TraitSystem traitSystem;

    public Text traitTooltipSpace;
    public Text traitNameSpace;

    public string traitName;
    public string traitTooltip;

    // Update is called once per frame
    public void SetTraitActive(bool isTraitEnabled)
    {
        ColorBlock colorBlock = button.colors;

        if (isTraitEnabled)
        {
            colorBlock.colorMultiplier = 5;
            button.colors = colorBlock;
        }
        else
        {
            colorBlock.colorMultiplier = 1;
            button.colors = colorBlock;
        }
    }

    public void HoverOverTooltip()
    {
        traitNameSpace.text = traitName;
        traitTooltipSpace.text = traitTooltip;
    }

    public void HoverOverExit()
    {
        traitNameSpace.text = "";
        traitTooltipSpace.text = "Hover over a trait to learn its effect.";
    }
}
