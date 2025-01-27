using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemyAttackState : State<EnemyBehaviour>
{
    FollowerBehaviour owner;
    float cooldownTimer;

    public override void Enter(EnemyBehaviour owner)
    {
        owner.Enemy.AI.isStopped = true;
        this.owner = owner as FollowerBehaviour;
        cooldownTimer = this.owner.AttackModule.Duration;
        this.owner.AttackModule.Attack(true);
    }

    public override void Exit(EnemyBehaviour owner)
    {
        this.owner.AttackModule.Attack(false);
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
            owner.StateMachine.ChangeState(new FollowerEnemyAttackExitState());
        }
    }
}
