using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAfterDetect : EnemyStatesBase
{
    public EnemyAfterDetect(Enemy enemy, EnemyStatesFactory enemyStateFactory) : base(enemy, enemyStateFactory)
    {

    }

    public override void EnterState()
    {
        Enemy.sus.gameObject.SetActive(true);

        Enemy.HasSuspectedAfterDetection = true;

        Enemy.Agent.ResetPath();
        Enemy.Agent.isStopped = false;

        SwitchStates(EStateFactory.Wait());
        
        Enemy.susTimerBg.gameObject.SetActive(false);
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }
}
