using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioEnemyAttackState : State<EnemyBehaviour>
{
    MarioBehaviour realOwner;
    DamageZone bullet;
    private float changeState;

    public override void Enter(EnemyBehaviour owner)
    {
        realOwner = owner as MarioBehaviour;

        PlayerManager player = GameManager.Instance.Player;
        bullet = Object.Instantiate(realOwner.Bullet, player.transform.position, Quaternion.identity);
        bullet.SetOwner(owner.gameObject);

        changeState = realOwner.AttackStateDuration;
    }

    public override void Exit(EnemyBehaviour owner)
    {
        Object.Destroy(bullet.gameObject);
    }

    public override void FixedUpdate(EnemyBehaviour owner)
    {
        
    }

    public override void Update(EnemyBehaviour owner)
    {
        if (changeState <= 0)
        {
            owner.StateMachine.ChangeState(new MarioEnemyIdleState());
        }
        else
        {
            changeState -= Time.deltaTime;
        }
    }
}
