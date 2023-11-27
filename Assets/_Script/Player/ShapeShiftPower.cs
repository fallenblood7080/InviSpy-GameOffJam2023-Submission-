using System;
using UnityEngine;
using UnityEngine.Events;

public class ShapeShiftPower : MonoBehaviour
{

    private bool isSmall = false;
    private bool isChangingSize = false;

    [Header("Marker")]
    [SerializeField] private Transform playerSmallMark;
    [SerializeField] private Vector3 offset;
    [Space(5f)]
    [SerializeField] private float smallSize , bigSize = 1f;
    [SerializeField] private bool useOrginalScale;

    [SerializeField] private float timeRequiredToShift;
    [SerializeField] private float timeLimitBeingSmall;
    [SerializeField] private float shiftPowerCooldown;

    [SerializeField] private LeanTweenType easeType;
    [SerializeField] private UnityEvent onShrink,onExpand;
    

    #region PROPERTY
    public UnityEvent OnShrink => onShrink;
    public UnityEvent OnExpand => onExpand;
    public bool IsCurrentlySmall => isSmall;
    public float TimeLimitBeingSmall => timeLimitBeingSmall;
    public float ShiftPowerCooldown => shiftPowerCooldown;
    public float TimeRequiredToShift => timeRequiredToShift;
    public float TimeSpentInSmall { get; private set; }
    public float NextTimeToUnlock { get; private set; }
    #endregion

    private void Start()
    {
        bigSize = useOrginalScale ? transform.localScale.x : bigSize;
    }

    void Update()
    {
        playerSmallMark.position = new(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z + offset.z);

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
            TimeSpentInSmall += Time.deltaTime;
            if (TimeSpentInSmall >= timeLimitBeingSmall)
            {
                Expand();
                TimeSpentInSmall = 0;
            }
        }
        else
        {
            NextTimeToUnlock += Time.deltaTime;
            if (NextTimeToUnlock >= ShiftPowerCooldown) NextTimeToUnlock = ShiftPowerCooldown;
        }
    }

    private void Expand()
    {
        transform.position = new(transform.position.x, transform.position.y + 2, transform.position.z);
        isChangingSize = true;
        LeanTween.scale(transform.gameObject, new(bigSize, bigSize, bigSize), timeRequiredToShift)
                 .setEase(easeType)
                 .setOnComplete(() => 
                 { 
                     isSmall = false; 
                     isChangingSize = false; 
                     TimeSpentInSmall = 0;
                     OnExpand?.Invoke();
                 });
    }

    private void Shrink()
    {
        if (NextTimeToUnlock >= ShiftPowerCooldown)
        {
            isChangingSize = true;
            LeanTween.scale(transform.gameObject, new(smallSize, smallSize, smallSize), timeRequiredToShift)
                     .setEase(easeType)
                     .setOnComplete(() => 
                     { 
                         isSmall = true; 
                         isChangingSize = false; 
                         NextTimeToUnlock = 0;
                         OnShrink?.Invoke();
                     }); 
        }
    }
}