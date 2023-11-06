using System;
using UnityEngine;
using UnityEngine.Events;

public class ShapeShiftPower : MonoBehaviour
{

    [HideInInspector] public bool isSmall = false;
    private bool isChangingSize = false;
    private float smallTime;
    private float nextTimeToUnlockShiftPower;

    [SerializeField] private float smallSize , bigSize = 1f;
    [SerializeField] private float timeRequiredToShift;
    [SerializeField] private float timeLimitForBeingSmall;
    [SerializeField] private float shiftPowerCoolDown;
    [SerializeField] private LeanTweenType easeType;

    public bool IsCurrentlySmall => isSmall;

    [SerializeField] private UnityEvent onShrink,onExpand;
    public UnityEvent OnShrink => onShrink;
    public UnityEvent OnExpand => onExpand;

    void Update()
    {

        if (InputManager.GetInstance.IsChangeSizePressed)
        {
            if (isChangingSize == false)
            {
                Action sizeAction = isSmall ? Expand : Shrink;
                sizeAction?.Invoke();
            }
        }

        if (isSmall)
        {
            smallTime += Time.deltaTime;
            if (smallTime >= timeLimitForBeingSmall)
            {
                Expand();
            }
        }
        else
        {
            nextTimeToUnlockShiftPower += Time.deltaTime;
            if (nextTimeToUnlockShiftPower >= shiftPowerCoolDown) nextTimeToUnlockShiftPower = shiftPowerCoolDown;
        }
    }

    private void Expand()
    {
        isChangingSize = true;
        LeanTween.scale(transform.gameObject, new(bigSize, bigSize, bigSize), timeRequiredToShift)
                 .setEase(easeType)
                 .setOnComplete(() => 
                 { 
                     isSmall = false; 
                     isChangingSize = false; 
                     smallTime = 0;
                     OnExpand?.Invoke();
                 });
    }

    private void Shrink()
    {
        if (nextTimeToUnlockShiftPower >= shiftPowerCoolDown)
        {
            isChangingSize = true;
            LeanTween.scale(transform.gameObject, new(smallSize, smallSize, smallSize), timeRequiredToShift)
                     .setEase(easeType)
                     .setOnComplete(() => 
                     { 
                         isSmall = true; 
                         isChangingSize = false; 
                         nextTimeToUnlockShiftPower = 0;
                         OnShrink?.Invoke();
                     }); 
        }
    }
}