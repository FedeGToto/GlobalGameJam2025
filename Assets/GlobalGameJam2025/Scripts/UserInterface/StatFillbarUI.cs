using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// CODICE CESSO, DA RISCRIVERE IN FUTURO
public class StatFillbarUI : MonoBehaviour
{
    [SerializeField] private Image fillbar;
    [SerializeField] private StatType type;

    [SerializeField] private PlayerStats stats;
    private Stat trackedStat;
    private float maxStat;

    private void Start()
    {
        trackedStat = stats.GetStat(type);

        trackedStat.ValueChanged += TrackedStat_ValueChanged;
        TrackedStat_ValueChanged();

        stats.OnShieldChanged.AddListener(ShieldChanged);

        ShieldChanged(maxStat);
    }

    private void ShieldChanged(float currentShield)
    {
        float fillAmount = (float) currentShield / maxStat;

        fillbar.DOFillAmount(fillAmount, 1f);
    }

    private void TrackedStat_ValueChanged()
    {
        maxStat = trackedStat.Value;
    }
}
