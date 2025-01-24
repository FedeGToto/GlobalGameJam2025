using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorrettaEnemyIdleState : State<EnemyBehaviour>
{
    TorrettaBehaviour owner;
    PlayerManager player;
    private float nextStateTimer;

    public override void Enter(EnemyBehaviour owner)
    {
        this.owner = owner as TorrettaBehaviour;

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
            owner.StateMachine.ChangeState(new TorrettaEnemyWalkState());
        }
        else
        {
            nextStateTimer -= Time.deltaTime;
        }

        if (distance < this.owner.AttackDistance)
        {
            owner.StateMachine.ChangeState(new TorrettaEnemyAttackState());
        }
    }
}
