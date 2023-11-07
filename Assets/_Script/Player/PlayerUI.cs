using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Powers")]
    [SerializeField] private Image BeingSmall;

    private ShapeShiftPower power;

    private float timeElapsed;

    private void Start() 
    {
        power = GetComponent<ShapeShiftPower>();
    }

    private void Update() 
    {
        if (!power.isSmall)
        {
            BeingSmall.fillAmount =  power.nextTimeToUnlockShiftPower / power.shiftPowerCoolDown;
        }
        else
        {
            BeingSmall.fillAmount = 1 - power.smallTime / power.timeLimitForBeingSmall;
        }
    }
}