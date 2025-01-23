using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemyWalkState : State<EnemyBehaviour>
{
    FollowerBehaviour owner;

    public override void Enter(EnemyBehaviour owner)
    {
        this.owner = owner as FollowerBehaviour;
    }

    public override void Exit(EnemyBehaviour owner)
    {
        
    }

    public override void FixedUpdate(EnemyBehaviour owner)
    {
        
    }

    public override void Update(EnemyBehaviour owner)
    {
        PlayerManager player = GameManager.Instance.Player;

        owner.Enemy.AI.SetDestination(player.transform.position);
        float distance = Vector3.Distance(owner.transform.position, player.transform.position);

        if (distance <= this.owner.AttackDistance)
        {
            owner.StateMachine.ChangeState(new FollowerEnemyPrepareToAttackState());
        }
    }
}
