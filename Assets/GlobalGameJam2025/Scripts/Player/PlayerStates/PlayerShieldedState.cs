using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerShieldedState : State<PlayerManager>
{
    private PlayerManager owner;

    public override void Enter(PlayerManager owner)
    {
        this.owner = owner;

        owner.PlayerAnimation.UpdateWalk(false);

        owner.Shield(true);
        owner.Stats.ShieldUp = true;

        owner.Stats.OnShieldChanged.AddListener(ShieldChanged);
    }

    public override void Exit(PlayerManager owner)
    {
        owner.Shield(false);
        owner.Stats.ShieldUp = false;

        owner.Stats.OnShieldChanged.RemoveListener(ShieldChanged);
    }

    public override void FixedUpdate(PlayerManager owner)
    {
        
    }

    public override void Update(PlayerManager owner)
    {
        if (!InputManager.SHIELD)
        {
            owner.StateMachine.ChangeState(new PlayerNormalState());
        }
    }

    private void ShieldChanged(float currentShield)
    {
        if (currentShield <= 0)
            owner.StateMachine.ChangeState(new PlayerNormalState());
    }
}
