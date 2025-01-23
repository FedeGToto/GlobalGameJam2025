using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackTrigger;
    [SerializeField] private float duration;

    public float Duration => duration;

    public void Attack(bool value)
    {
        attackTrigger.SetActive(value);
    }
}
