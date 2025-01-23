using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBehaviour : EnemyBehaviour
{
    public float AttackDistance = 2f;
    public EnemyAttack AttackModule;

    protected override void Start()
    {
        base.Start();

        StateMachine.ChangeState(new FollowerEnemyWalkState());
    }
}
