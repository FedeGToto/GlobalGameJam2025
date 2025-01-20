using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private string horizontalMoveName = "Horizontal";
    [SerializeField] private string verticalMoveName = "Vertical";
    [SerializeField] private string attackName = "Attack";
    [SerializeField] private string shieldName = "Shield";
    [SerializeField] private string pauseName = "Pause";

    public static float HORIZONTAL_MOVE;
    public static float VERTICAL_MOVE;
    public static bool ATTACK;
    public static bool SHIELD;
    public static bool PAUSE;

    private Player playerControls;

    private void Awake()
    {
        playerControls = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        HORIZONTAL_MOVE = playerControls.GetAxisRaw(horizontalMoveName);
        VERTICAL_MOVE = playerControls.GetAxisRaw(verticalMoveName);

        ATTACK = playerControls.GetButton(attackName);
        SHIELD = playerControls.GetButton(shieldName);

        PAUSE = playerControls.GetButton(pauseName);
    }
}
