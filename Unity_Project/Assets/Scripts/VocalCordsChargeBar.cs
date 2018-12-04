using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VocalCordsChargeBar : MonoBehaviour
{
    [SerializeField]
    private float fill;

    [SerializeField]
    private Image content;

    public PlayerController player;

    public float Value
    {
        set
        {
            fill = SetValue(value, 0, player.maxCharge, 0, 1);
        }
    }

    void Update()
    {
        if (player.chargeTime >= player.maxCharge)
        {
            content.color = Color.red;
        }
        else
        {
            content.color = Color.white;
        }
        
        Value = player.chargeTime;
        BarUpdate();
    }

    private void BarUpdate()
    {
        if (fill != content.fillAmount)
        {
            content.fillAmount = fill;
        }
    }
    private float SetValue(float _value, float minValue, float maxValue, float minOutput, float maxOutput)
    {
        return (_value - minValue) * (maxOutput - minOutput) / (maxValue - minValue) + minOutput;
    }
}