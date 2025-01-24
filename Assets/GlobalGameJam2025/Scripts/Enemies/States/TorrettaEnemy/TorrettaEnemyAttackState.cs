using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorrettaEnemyAttackState : State<EnemyBehaviour>
{
    TorrettaBehaviour realOwner;
    Bullet bullet;
    private float changeState;

    public override void Enter(EnemyBehaviour owner)
    {
        realOwner = owner as TorrettaBehaviour;
        bullet = Object.Instantiate(realOwner.Bullet, realOwner.firePoint.position, realOwner.firePoint.rotation);
        bullet.SetOwner(owner.gameObject);
        bullet.Shoot();

        changeState = realOwner.AttackStateDuration;
    }

    public override void Exit(EnemyBehaviour owner)
    {
    }

    public override void FixedUpdate(EnemyBehaviour owner)
    {
        
    }

    public override void Update(EnemyBehaviour owner)
    {
        if (changeState <= 0)
        {
            owner.StateMachine.ChangeState(new TorrettaEnemyIdleState());
        }
        else
        {
            changeState -= Time.deltaTime;
        }
    }
}
