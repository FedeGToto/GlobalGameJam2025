using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerBehaviour : EnemyBehaviour
{
    [Header("Timers")]
    public float IdleStateDuration;

    [Header("Attack")]
    public EnemyAttack Attack;


    protected override void Start()
    {
        base.Start();
        StateMachine.ChangeState(new ChargerEnemyIdleState());
    }
}
