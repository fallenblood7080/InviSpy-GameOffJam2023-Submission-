using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : EnemyStatesBase
{
    public EnemyDetect(Enemy enemy, EnemyStatesFactory enemyStateFactory) : base(enemy, enemyStateFactory)
    {

    }

    public override void EnterState()
    {
        Enemy.Agent.ResetPath();
        Enemy.Agent.isStopped = true;
        Enemy.ChangeeAnimationState(AnimationState.Idle);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        //var targetRotation = Quaternion.LookRotation(EnemyManager.Instance.player.transform.position - Enemy.transform.position);
        //Enemy.transform.rotation = Quaternion.Slerp(Enemy.transform.rotation, targetRotation, 2f * Time.deltaTime);

        Enemy.susTimerBg.gameObject.SetActive(true);
        Enemy.susTimer.fillAmount = Enemy.timeElapsedWhenDetected / Enemy.maxTimeDetection;

        if (Enemy.timeElapsedWhenDetected < 0f || Enemy.timeElapsedWhenDetected == 0f)
        {
            SwitchStates(EStateFactory.AfterDetect()); 
        }
    }
}