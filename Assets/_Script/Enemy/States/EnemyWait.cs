using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWait : EnemyStatesBase
{
    public float randomWaitTime;

    public EnemyWait(Enemy enemy, EnemyStatesFactory enemyStateFactory) : base(enemy, enemyStateFactory)
    {
    }
    
    public override void EnterState()
    {
        Enemy.LastWaitRoutine = Enemy.StartCoroutine(DelayWaitForPetrol());
    }
    
    IEnumerator DelayWaitForPetrol()
    {
        randomWaitTime = Random.Range(Enemy.MinWaitTime, Enemy.MaxWaitTime);
        yield return new WaitForSeconds(randomWaitTime);
        Enemy.sus.gameObject.SetActive(false);
        Enemy.hasSuspected = false;
        SwitchStates(EStateFactory.Petrol());
    }

    #region UnUsed Functions
    public override void ExitState()
    {

    }
    public override void UpdateState()
    {

    }
        
    #endregion
}