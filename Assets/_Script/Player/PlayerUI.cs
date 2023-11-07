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
        BeingSmall.fillAmount = !power.IsCurrentlySmall ? power.NextTimeToUnlock / power.ShiftPowerCooldown : 1 - power.TimeSpentInSmall / power.TimeLimitBeingSmall;
    }
}