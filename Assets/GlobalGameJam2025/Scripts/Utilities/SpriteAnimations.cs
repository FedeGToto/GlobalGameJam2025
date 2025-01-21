using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimations : MonoBehaviour
{
    [Header("Damage Animation")]
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float damageHalfDuration = 0.1f;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void DamageAnimation()
    {
        sprite.DOColor(damageColor, damageHalfDuration).SetLoops(2, LoopType.Yoyo);
    }
}
