using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchingState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(Enemy enemy)
    {
        if (enemy.hitPoints <= 0)
        {
            enemy.TransitionToState(enemy.DeathState);
        }
    }
}
