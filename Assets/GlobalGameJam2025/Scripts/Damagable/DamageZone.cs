using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DamageTo { Enemy, Player, Everyone }

public class DamageZone : MonoBehaviour
{

    [SerializeField] private bool cacheDamage;
    [SerializeField] private GameObject owner;
    [SerializeField] private DamageTo damageTo;

    public UnityEvent<IDamagable> DamageDealt;

    private float cachedDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (!CompareEntity(other.GetComponent<Entity>())) return; 

        if (other.TryGetComponent(out IDamagable damagable))
        {
            DamageDealt?.Invoke(damagable);

            float damage = cacheDamage ? cachedDamage : owner.GetComponent<IAttacker>().GetDamageValue();

            damagable.TakeDamage(damage);
        }
    }

    public bool CompareEntity(Entity entity)
    {
        return damageTo switch
        {
            DamageTo.Enemy => entity is Enemy,
            DamageTo.Player => entity is PlayerManager,
            _=> true
        };
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;

        if (cacheDamage)
            cachedDamage = owner.GetComponent<IAttacker>().GetDamageValue();
    }

    public void EndAnimation()
    {
        if (owner == null) Destroy(gameObject);
    }
}
