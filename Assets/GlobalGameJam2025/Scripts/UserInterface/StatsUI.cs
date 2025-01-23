using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private GameObject statsCanvas;

    private void Update()
    {
        statsCanvas.SetActive(InputManager.STATS);
    }
}
