using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemyWalkState : State<EnemyBehaviour>
{
    public override void Enter(EnemyBehaviour owner)
    {
        
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

        owner.CheckForNextState(distance);
    }
}
