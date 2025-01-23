using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private string horizontalMoveName = "Horizontal";
    [SerializeField] private string verticalMoveName = "Vertical";
    [SerializeField] private string attackName = "Attack";
    [SerializeField] private string shieldName = "Shield";
    [SerializeField] private string dashName = "Dash";
    [SerializeField] private string statsName = "Stats";

    public static float HORIZONTAL_MOVE;
    public static float VERTICAL_MOVE;
    public static bool ATTACK;
    public static bool SHIELD;
    public static bool DASH;
    public static bool STATS;

    private Player playerControls;

    public override void Awake()
    {
        base.Awake();
        playerControls = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        HORIZONTAL_MOVE = playerControls.GetAxisRaw(horizontalMoveName);
        VERTICAL_MOVE = playerControls.GetAxisRaw(verticalMoveName);

        ATTACK = playerControls.GetButtonDown(attackName);
        SHIELD = playerControls.GetButton(shieldName);
        DASH = playerControls.GetButtonDown(dashName);

        STATS = playerControls.GetButton(statsName);
    }

    public bool IsUsingController()
    {
        // Implement controller support
        return false;
    }
}
