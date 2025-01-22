using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : Entity, IDamagable, IAttacker
{
    [SerializeField] private NavMeshAgent aiAgent;

    [Header("Stats")]
    [SerializeField] private float healthPoints;
    [SerializeField] private float damage;

    [Header("VFX")]
    [SerializeField] private ParticleSystem deathPS;

    public UnityEvent OnDamageTaken;
    public UnityEvent OnDied;

    private float currentHP;

    public override void Start()
    {
        base.Start();

        currentHP = healthPoints;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            GameEvents.current.Kill();

            OnDied?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            OnDamageTaken?.Invoke();
        }
    }

    public float GetDamageValue()
    {
        return damage;
    }

    public void PlayDeathEffect()
    {
        ParticleSystem GO = Instantiate(deathPS, transform.position, transform.rotation);
    }

    public NavMeshAgent AI => aiAgent;
}
