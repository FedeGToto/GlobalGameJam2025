using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool cacheDamage;
    [SerializeField] private GameObject owner;
    [SerializeField] private DamageTo damageTo;

    public UnityEvent<IDamagable> DamageDealt;

    private Rigidbody rb;
    private float cachedDamage;

    private void OnCollisionEnter(Collision collision)
    {
        if (CompareEntity(collision.gameObject.GetComponent<Entity>()))
        {

            if (collision.gameObject.TryGetComponent(out IDamagable damagable))
            {
                DamageDealt?.Invoke(damagable);

                float damage = cacheDamage ? cachedDamage : owner.GetComponent<IAttacker>().GetDamageValue();

                damagable.TakeDamage(damage);
            }
        }

        Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }

    public void Shoot(float damage = -1f)
    {
        if (damage > 0f)
            cachedDamage = damage;

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Force);
    }

    public bool CompareEntity(Entity entity)
    {
        return damageTo switch
        {
            DamageTo.Enemy => entity is Enemy,
            DamageTo.Player => entity is PlayerManager,
            _ => true
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
