using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour, IDamagable, IAttacker
{
    public bool ShieldUp;
    public float CurrentShield { get; private set; }
    public float DamageMultiplier = 1;

    private float shieldCooldown;

    public UnityEvent OnDamageTaken;
    public UnityEvent<float> OnShieldChanged;
    public UnityEvent OnDie;

    public Stat Speed;
    public Stat DashCooldown;
    public Stat Attack;
    public Stat AttackSpeed;
    public Stat MaxShield;
    public Stat ShieldCooldown;
    public Stat ShieldRegen;

    private void Awake()
    {
        Speed = new Stat(5f);

        DashCooldown = new Stat(1f);

        Attack = new Stat(5f);
        AttackSpeed = new Stat(0.5f);

        MaxShield = new Stat(100f);
        ShieldCooldown = new Stat(5f);
        ShieldRegen = new Stat(5f);
    }

    private void Start()
    {
        CurrentShield = MaxShield.Value;
    }

    private void Update()
    {
        if (ShieldUp && CurrentShield < MaxShield.Value)
        {
            if (shieldCooldown > 0)
            {
                shieldCooldown -= Time.deltaTime;
            }
            else
            {
                CurrentShield += ShieldRegen.Value * Time.deltaTime;
                OnShieldChanged?.Invoke(CurrentShield);
                if (CurrentShield > MaxShield.Value)
                    CurrentShield = MaxShield.Value;
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ShieldUp = true;
            TakeDamage(15);
        }
    }

    public Stat GetStat(StatType type)
    {
        return type switch
        {
            StatType.Speed => Speed,
            StatType.Attack => Attack,
            StatType.AttackSpeed => AttackSpeed,
            StatType.MaxShield => MaxShield,
            _ => null
        };
    }

    public void TakeDamage(float dmg)
    {
        if (!ShieldUp)
        {
            OnDie?.Invoke();
        }
        else
        {
            CurrentShield -= dmg;
            if (CurrentShield < 0)
                CurrentShield = 0;

            shieldCooldown = ShieldCooldown.Value;

            OnShieldChanged?.Invoke(CurrentShield);
            OnDamageTaken?.Invoke();
        }
    }

    public float GetDamageValue()
    {
        return Attack.Value;
    }
}

public enum StatType
{
    Speed,
    MaxShield,
    ShieldCooldown,
    ShieldRegen,
    Attack,
    AttackSpeed
}
