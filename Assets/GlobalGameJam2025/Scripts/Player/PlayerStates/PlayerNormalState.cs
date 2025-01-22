using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalState : State<PlayerManager>
{
    public override void Enter(PlayerManager owner)
    {
    }

    public override void Exit(PlayerManager owner)
    {
        
    }

    public override void FixedUpdate(PlayerManager owner)
    {
        Vector3 movement = owner.Stats.Speed.Value * Time.fixedDeltaTime * new Vector3(owner.MoveDirection.x, 0, owner.MoveDirection.y).normalized;
        owner.Rigidbody.MovePosition(owner.Rigidbody.position + movement);
    }

    public override void Update(PlayerManager owner)
    {
        owner.MoveDirection = new(InputManager.HORIZONTAL_MOVE, InputManager.VERTICAL_MOVE);

        owner.PlayerAnimation.UpdateWalk(owner.MoveDirection != Vector2.zero);
        owner.PlayerAnimation.FlipSprite(owner.MoveDirection);

        if (InputManager.ATTACK)
        {
            owner.StateMachine.ChangeState(new PlayerAttackState());
        }

        if (InputManager.SHIELD && owner.Stats.CurrentShield > 0)
        {
            owner.StateMachine.ChangeState(new PlayerShieldedState());
        }

        if (InputManager.DASH && owner.DashTimer <= 0)
        {
            owner.StateMachine.ChangeState(new PlayerDashState());
        }
    }
}
