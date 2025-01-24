using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundText;

    private void Start()
    {
        GameManager.Instance.Level.OnLevelStarted.AddListener((x) =>
        {
            roundText.text = "Round " + (x + 1);
        });
    }
}
