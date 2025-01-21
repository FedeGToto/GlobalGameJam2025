using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : Entity, IDamagable, IAttacker
{
    [SerializeField] private NavMeshAgent aiAgent;

    [SerializeField] private float damage;

    public UnityEvent OnDamageTaken;

    public override void Start()
    {
        base.Start();
    }

    public void TakeDamage(float damage)
    {
        OnDamageTaken?.Invoke();
    }

    public float GetDamageValue()
    {
        return damage;
    }

    public NavMeshAgent AI => aiAgent;
}
