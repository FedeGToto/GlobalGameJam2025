using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemyAttackExitState : State<EnemyBehaviour>
{
    FollowerBehaviour owner;
    float cooldownTimer;

    public override void Enter(EnemyBehaviour owner)
    {
        this.owner = owner as FollowerBehaviour;
        cooldownTimer = this.owner.AttackModule.ExitDuration;
    }

    public override void Exit(EnemyBehaviour owner)
    {
        owner.Enemy.AI.isStopped = false;
    }

    public override void FixedUpdate(EnemyBehaviour owner)
    {

    }

    public override void Update(EnemyBehaviour owner)
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            owner.StateMachine.ChangeState(new FollowerEnemyWalkState());
        }
    }
}
