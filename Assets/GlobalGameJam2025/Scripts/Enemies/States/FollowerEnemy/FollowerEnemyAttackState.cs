using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemyAttackState : State<EnemyBehaviour>
{
    FollowerBehaviour owner;
    float cooldownTimer;

    public override void Enter(EnemyBehaviour owner)
    {
        this.owner = owner as FollowerBehaviour;
        this.owner.AttackModule.Attack(true);
        cooldownTimer = this.owner.AttackModule.Duration;
    }

    public override void Exit(EnemyBehaviour owner)
    {
        this.owner.AttackModule.Attack(false);
    }

    public override void FixedUpdate(EnemyBehaviour owner)
    {

    }

    public override void Update(EnemyBehaviour owner)
    {
        if (cooldownTimer <= 0)
        {
            owner.StateMachine.ChangeState(new FollowerEnemyWalkState());
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
