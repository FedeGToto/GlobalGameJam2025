using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private bool cacheDamage;
    [SerializeField] private GameObject owner;

    public UnityEvent<IDamagable> DamageDealt;

    private float cachedDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner) return;

        if (other.TryGetComponent(out IDamagable damagable))
        {
            DamageDealt?.Invoke(damagable);

            float damage = cacheDamage ? cachedDamage : owner.GetComponent<IAttacker>().GetDamageValue();

            damagable.TakeDamage(damage);
        }
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;

        if (cacheDamage)
            owner.GetComponent<IAttacker>().GetDamageValue();
    }

    public void EndAnimation()
    {
        if (owner == null) Destroy(gameObject);
    }
}
