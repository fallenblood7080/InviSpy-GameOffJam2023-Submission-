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
    }
