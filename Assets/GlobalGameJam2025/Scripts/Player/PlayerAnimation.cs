using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Animations")]
    [SerializeField] private string walkString = "Speed";

    private SpriteRenderer renderer;
    private int walkHash;

    private void Start()
    {
        walkHash = Animator.StringToHash(walkString);
        renderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateWalk(bool speed)
    {
        animator.SetBool(walkHash, speed);
    }

    public void FlipSprite(Vector2 moveDirection)
    {
        if (moveDirection.x < 0 && !renderer.flipX)
            renderer.flipX = true;
        if (moveDirection.x > 0 && renderer.flipX)
            renderer.flipX = false;
    }
}
