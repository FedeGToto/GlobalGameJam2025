using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioBehaviour : EnemyBehaviour
{
    [Header("Movement")]
    public float MoveRadius;

    [Header("Shoot")]
    public float AttackDistance;
    public DamageZone Bullet;

    [Header("Timers")]
    public float IdleStateDuration;
    public float AttackStateDuration;


    protected override void Start()
    {
        base.Start();
        StateMachine.ChangeState(new MarioEnemyIdleState());
    }
}
