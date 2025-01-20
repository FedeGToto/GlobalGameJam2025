using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private StatType scaleStat = StatType.Speed;

    private Rigidbody rb;
    private PlayerStats playerStats;

    private Vector2 moveDirection;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveDirection = new (InputManager.HORIZONTAL_MOVE, InputManager.VERTICAL_MOVE);
    }

    private void FixedUpdate()
    {
        Vector3 movement = playerStats.GetStat(scaleStat).Value * Time.fixedDeltaTime * new Vector3(moveDirection.x, 0, moveDirection.y).normalized;
        rb.MovePosition(rb.position + movement);
    }
}
