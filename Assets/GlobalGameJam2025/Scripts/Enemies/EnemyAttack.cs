using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackTrigger;
    [SerializeField] private ParticleSystem attackPreparation;
    [SerializeField] private float duration;
    [SerializeField] private float windUpDuration;
    [SerializeField] private float exitDuration;


    public float Duration => duration;
    public float WindUpDuration => windUpDuration;
    public float ExitDuration => exitDuration;

    public void Attack(bool value)
    {
        attackTrigger.SetActive(value);
    }

    public void ShowAttackPreparation(bool value)
    {
        attackPreparation.Play();
    }
}
