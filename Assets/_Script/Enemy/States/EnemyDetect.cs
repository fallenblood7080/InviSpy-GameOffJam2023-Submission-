using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyDetect : EnemyStatesBase
{
    public EnemyDetect(Enemy enemy, EnemyStatesFactory enemyStateFactory) : base(enemy, enemyStateFactory)
    {

    }

    public override void EnterState()
    {
        Detecting();
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    private async void Detecting()
    {
        Enemy.Agent.ResetPath();
        Enemy.Agent.isStopped = true;

        await Task.Delay(2000);
    }
}