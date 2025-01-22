using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    public UnityEvent<IDamagable> DamageDealt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner) return;

        if (other.TryGetComponent(out IDamagable damagable))
        {
            DamageDealt?.Invoke(damagable);
            damagable.TakeDamage(owner.GetComponent<IAttacker>().GetDamageValue());
        }
    }
}
