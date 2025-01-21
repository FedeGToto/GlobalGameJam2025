using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Entity
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private PlayerAim aim;
    [SerializeField] private PlayerInventory inventory;

    [Header("Attack")]
    [SerializeField] private GameObject attackTrigger;
    public float AttackDuration = 0.3f;

    [Header("Shield")]
    [SerializeField] private GameObject shieldObject;

    [Header("Dash")]
    public float DashTime = 0.3f;
    public float DashMultiplier = 1.2f;

    [Header("Animations")]
    [SerializeField] private PlayerAnimation playerAnimation;
    public PlayerAnimation PlayerAnimation => playerAnimation;

    public StateMachine<PlayerManager> StateMachine { get; protected set; }
    public Vector2 MoveDirection;

    public override void Start()
    {
        base.Start();
        StateMachine = new StateMachine<PlayerManager>(this);

        StateMachine.ChangeState(new PlayerNormalState());
    }

    public virtual void Update()
    {
        StateMachine.Update();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    public PlayerStats Stats => stats;
    public PlayerAim Aim => aim;
    public PlayerInventory Inventory => inventory;

    public void Attack(bool value)
    {
        attackTrigger.SetActive(value);
    }

    public void Shield(bool value)
    {
        shieldObject.SetActive(value);
    }
}
