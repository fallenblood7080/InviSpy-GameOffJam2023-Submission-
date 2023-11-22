using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyStatesBase
{
    public EnemyChase(Enemy enemy, EnemyStatesFactory enemyStateFactory) : base(enemy, enemyStateFactory)
    {

    }

    public override void EnterState()
    {
        Enemy.Agent.ResetPath();
        Enemy.Agent.speed = Enemy.ChaseSpeed;
        Enemy.ChangeeAnimationState(AnimationState.Run);
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        Enemy.Agent.SetDestination(EnemyManager.Instance.player.transform.position);
        Enemy.sus.gameObject.SetActive(true);

        if (Enemy.isChasing && Enemy.hasDetected)
        {
            SwitchStates(EStateFactory.Detect());
            Enemy.sus.gameObject.SetActive(false);
            Enemy.isChasing = false;
        }
        if (Enemy.isChasing && !EnemyManager.Instance.isCreatingNoise)
        {
            SwitchStates(EStateFactory.AfterChase());
        }
    }
}