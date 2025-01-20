using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public bool ShieldUp;
    public float CurrentShield { get; private set; }

    private float shieldCooldown;

    public UnityEvent OnDamageTaken;
    public UnityEvent<float> OnShieldChanged;
    public UnityEvent OnDie;

    public Stat Speed;
    public Stat MaxShield;
    public Stat ShieldCooldown;
    public Stat ShieldRegen;

    private void Awake()
    {
        Speed = new Stat(5f);

        MaxShield = new Stat(1000f);
        ShieldCooldown = new Stat(5f);
        ShieldRegen = new Stat(100f);
    }

    private void Start()
    {
        CurrentShield = MaxShield.Value;
    }

    private void Update()
    {
        if (CurrentShield > 0 && CurrentShield < MaxShield.Value)
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
            StatType.MaxShield => MaxShield,
            _ => null
        };
    }

    public void TakeDamage(int dmg)
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
}

public enum StatType
{
    Speed,
    MaxShield,
    ShieldCooldown,
    ShieldRegen
}
