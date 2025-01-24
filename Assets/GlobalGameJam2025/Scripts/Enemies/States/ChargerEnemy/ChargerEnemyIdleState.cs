using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemyIdleState : State<EnemyBehaviour>
{
    ChargerBehaviour owner;
    private float nextStateTimer;

    public override void Enter(EnemyBehaviour owner)
    {
        this.owner = owner as ChargerBehaviour;
        nextStateTimer = this.owner.IdleStateDuration;
    }

    public override void Exit(EnemyBehaviour owner)
    {

    }

    public override void FixedUpdate(EnemyBehaviour owner)
    {

    }

    public override void Update(EnemyBehaviour owner)
    {
        if (nextStateTimer <= 0)
        {
            owner.StateMachine.ChangeState(new ChargerEnemyAttackState());
        }
        else
        {
            nextStateTimer -= Time.deltaTime;
        }
    }
}
