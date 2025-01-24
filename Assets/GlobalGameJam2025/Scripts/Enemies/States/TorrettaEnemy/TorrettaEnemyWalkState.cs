using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TorrettaEnemyWalkState : State<EnemyBehaviour>
{
    TorrettaBehaviour owner;
    Vector3 destination;

    public override void Enter(EnemyBehaviour owner)
    {
        this.owner = owner as TorrettaBehaviour;

        Vector3 point = owner.transform.position + Random.insideUnitSphere * this.owner.MoveRadius;
        point.y = 0f;

        if (NavMesh.SamplePosition(point, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            destination = hit.position;
        }

        owner.Enemy.AI.SetDestination(destination);
    }

    public override void Exit(EnemyBehaviour owner)
    {

    }

    public override void FixedUpdate(EnemyBehaviour owner)
    {
        
    }

    public override void Update(EnemyBehaviour owner)
    {
        float destinationDistance = Vector3.Distance(owner.transform.position, destination);

        if (destinationDistance <= 1f)
        {
            owner.StateMachine.ChangeState(new TorrettaEnemyIdleState());
        }
    }
}
