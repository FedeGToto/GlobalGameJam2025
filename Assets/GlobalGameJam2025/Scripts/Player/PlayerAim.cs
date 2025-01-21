using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public bool IsAiming = true;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private GameObject aimArrow;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (IsAiming)
            Aim();
    }

    private void Aim()
    {
        if (!InputManager.Instance.IsUsingController())
        {
            var (success, position) = GetMousePosition();

            if (success)
            {
                var direction = position - transform.position;
                direction.y = 0;
                aimArrow.transform.forward = direction;
            }
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, groundMask))
        {
            return (success: true, hit.point);
        }
        else
            return (success: false, position: Vector3.zero);
    }
}
