using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatesFactory : MonoBehaviour
    {
        public Enemy Enemy;

        public EnemyStatesFactory(Enemy enemy)
        {
            Enemy = enemy;
        }

        // States
        public EnemyPatrol Petrol() { return new EnemyPatrol(Enemy, this); }
        public EnemyWait Wait() { return new EnemyWait(Enemy, this); }
        public EnemyDetect Detect() { return new EnemyDetect(Enemy, this); }
        public EnemyChase Chase() { return new EnemyChase(Enemy, this); }
        public EnemyAfterDetect AfterDetect() { return new EnemyAfterDetect(Enemy, this); }
        public EnemyAfterChase AfterChase() { return new EnemyAfterChase(Enemy, this); }
    }
