using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargerEnemyAttackState : State<EnemyBehaviour>
{
    ChargerBehaviour owner;
    Vector3 destination;

    public override void Enter(EnemyBehaviour owner)
    {
        this.owner = owner as ChargerBehaviour;

        Vector3 point = GameManager.Instance.Player.transform.position + Random.insideUnitSphere * 2f;
        point.y = 0f;

        if (NavMesh.SamplePosition(point, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            destination = hit.position;
        }

        owner.Enemy.AI.SetDestination(destination);

        this.owner.Attack.Attack(true);
    }

    public override void Exit(EnemyBehaviour owner)
    {
        this.owner.Attack.Attack(false);
    }

    public override void FixedUpdate(EnemyBehaviour owner)
    {
        
    }

    public override void Update(EnemyBehaviour owner)
    {
        float destinationDistance = Vector3.Distance(owner.transform.position, destination);

        if (destinationDistance <= 1f)
        {
            owner.StateMachine.ChangeState(new ChargerEnemyIdleState());
        }
    }
}
