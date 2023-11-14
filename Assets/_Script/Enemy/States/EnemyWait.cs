using System.Collections;
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
        Enemy.anim.SetFloat(SPEED_TAG, 0f);
    }
    
    IEnumerator DelayWaitForPetrol()
    {
        randomWaitTime = Random.Range(Enemy.MinWaitTime, Enemy.MaxWaitTime);

        yield return new WaitForSeconds(randomWaitTime);
        Enemy.sus.gameObject.SetActive(false);
        Enemy.HasSuspectedAfterDetection = false;

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

    #region Cached Properties

    private static readonly int SPEED_TAG = Animator.StringToHash("Speed");


    #endregion
}