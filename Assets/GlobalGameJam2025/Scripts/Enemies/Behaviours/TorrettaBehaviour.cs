using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorrettaBehaviour : EnemyBehaviour
{
    [Header("Movement")]
    public float MoveRadius;

    [Header("Shoot")]
    public AudioSource ShootSource;
    public float AttackDistance;
    public Bullet Bullet;
    public Transform firePoint;

    [Header("Timers")]
    public float IdleStateDuration;
    public float AttackStateDuration;


    protected override void Start()
    {
        base.Start();
        StateMachine.ChangeState(new TorrettaEnemyIdleState());
    }
}
