using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : State<PlayerManager>
{
    private float dashTimer;

    public override void Enter(PlayerManager owner)
    {
        dashTimer = owner.DashTime;
        owner.OnPlayerDash?.Invoke();
    }

    public override void Exit(PlayerManager owner)
    {
        owner.DashCooldown();
    }

    public override void FixedUpdate(PlayerManager owner)
    {
        Vector3 direction = new Vector3(owner.MoveDirection.x, 0, owner.MoveDirection.y).normalized;
        float force = owner.Stats.Speed.Value * owner.DashMultiplier * Time.fixedDeltaTime;

        if (!owner.Rigidbody.SweepTest(direction, out RaycastHit hit, 1))
        {
            Vector3 movement = force * direction;
            owner.Rigidbody.MovePosition(owner.Rigidbody.position + movement);
        }
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
