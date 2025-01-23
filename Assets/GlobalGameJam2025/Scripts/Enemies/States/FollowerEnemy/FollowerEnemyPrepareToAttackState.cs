using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemyPrepareToAttackState : State<EnemyBehaviour>
{
    FollowerBehaviour owner;
    float cooldownTimer;

    public override void Enter(EnemyBehaviour owner)
    {
        this.owner = owner as FollowerBehaviour;
        cooldownTimer = this.owner.AttackModule.WindUpDuration;
        this.owner.AttackModule.ShowAttackPreparation(true);
    }

    public override void Exit(EnemyBehaviour owner)
    {
    }

    public override void FixedUpdate(EnemyBehaviour owner)
    {

    }

    public override void Update(EnemyBehaviour owner)
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            owner.StateMachine.ChangeState(new FollowerEnemyAttackState());
        }
    }
}
