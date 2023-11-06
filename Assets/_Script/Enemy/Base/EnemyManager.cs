using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    #region Singlton
    public static EnemyManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public Transform[] WayPoints;
    public ShapeShiftPower playerPower;
}