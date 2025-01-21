using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : State<PlayerManager>
{
    private float dashTimer;

    public override void Enter(PlayerManager owner)
    {
        dashTimer = owner.DashTime;
    }

    public override void Exit(PlayerManager owner)
    {

    }

    public override void FixedUpdate(PlayerManager owner)
    {
        Vector3 movement = owner.Stats.Speed.Value * owner.DashMultiplier * Time.fixedDeltaTime * new Vector3(owner.MoveDirection.x, 0, owner.MoveDirection.y).normalized;
        owner.Rigidbody.MovePosition(owner.Rigidbody.position + movement);
    }

    public override void Update(PlayerManager owner)
    {
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
        else
        {
            owner.StateMachine.ChangeState(new PlayerNormalState());
        }
    }
}
