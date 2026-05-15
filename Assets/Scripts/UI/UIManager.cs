using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Элементы")]
    public TextMeshProUGUI waveText;

    private int currentWave = 1;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateWaveUI(int waveNumber)
    {
        currentWave = waveNumber;
        if (waveText != null)
            waveText.text = $"Wave {waveNumber}";
    }
}