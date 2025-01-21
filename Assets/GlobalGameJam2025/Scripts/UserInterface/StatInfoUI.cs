using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatInfoUI : MonoBehaviour
{
    [SerializeField] private StatType trackedStat;
    [SerializeField] private TextMeshProUGUI valueText;

    private Stat statToTrack;

    private void Start()
    {
        statToTrack = GameManager.Instance.Player.Stats.GetStat(trackedStat);
        statToTrack.ValueChanged += StatInfoUI_ValueChanged;

        StatInfoUI_ValueChanged();
    }

    private void StatInfoUI_ValueChanged()
    {
        valueText.text = statToTrack.Value.ToString();
    }
}
