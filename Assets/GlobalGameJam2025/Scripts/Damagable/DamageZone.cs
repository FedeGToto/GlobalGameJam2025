using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private GameObject owner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner) return;

        if (other.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(owner.GetComponent<IAttacker>().GetDamageValue());
        }
    }
}
