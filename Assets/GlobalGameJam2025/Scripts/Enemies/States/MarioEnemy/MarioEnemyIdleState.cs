using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioEnemyIdleState : State<EnemyBehaviour>
{
    MarioBehaviour owner;
    PlayerManager player;
    private float nextStateTimer;

    public override void Enter(EnemyBehaviour owner)
    {
        this.owner = owner as MarioBehaviour;

        player = GameManager.Instance.Player;
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
        float distance = Vector3.Distance(owner.transform.position, player.transform.position);

        if (nextStateTimer <= 0)
        {
            owner.StateMachine.ChangeState(new MarioEnemyWalkState());
        }
        else
        {
            nextStateTimer -= Time.deltaTime;
        }

        if (distance < this.owner.AttackDistance)
        {
            owner.StateMachine.ChangeState(new MarioEnemyAttackState());
        }
    }
}
