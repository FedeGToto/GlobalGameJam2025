using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    public StateMachine<EnemyBehaviour> StateMachine { get; private set; }

    protected virtual void Start()
    {
        StateMachine = new StateMachine<EnemyBehaviour>(this);
    }

    public Enemy Enemy => enemy;

    public virtual void Update()
    {
        StateMachine.Update();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    public virtual void CheckForNextState(float distance)
    {
        
    }
}
