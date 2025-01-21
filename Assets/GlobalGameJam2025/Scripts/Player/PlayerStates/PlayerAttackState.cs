using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : State<PlayerManager>
{
    private float attackDuration;

    public override void Enter(PlayerManager owner)
    {
        owner.PlayerAnimation.UpdateWalk(false);
        owner.Aim.IsAiming = false;
        attackDuration = owner.Stats.AttackSpeed.Value;
        owner.Attack(true);
    }

    public override void Exit(PlayerManager owner)
    {
        owner.Aim.IsAiming = true;
        owner.Attack(false);
    }

    public override void FixedUpdate(PlayerManager owner)
    {
        
    }

    public override void Update(PlayerManager owner)
    {
        attackDuration -= Time.deltaTime;
        if (attackDuration <= 0)
        {
            owner.StateMachine.ChangeState(new PlayerNormalState());
        }
    }
}
