using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Здоровье")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI")]
    public TextMeshProUGUI healthText;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log($"[PlayerHealth] Получено {damage} урона. Остаток: {currentHealth}/{maxHealth}");

        UpdateUI();

        if (currentHealth <= 0)
            Die();
    }

    public void IncreaseHelth(int value)
    {
        maxHealth += value;
        UpdateUI();
    }

    private void UpdateUI()
{
    Debug.Log("HP UI update: " + currentHealth);

    if (healthText != null)
    {
        healthText.text = $"HP: {currentHealth} / {maxHealth}";
    }
}
void Die()
{
    Debug.Log("💀 ИГРОК УМЕР! Вызываем Game Over");

    if (GameOverManager.Instance != null)
    {
        GameOverManager.Instance.ShowGameOver();
        Debug.Log("GameOverManager.ShowGameOver() вызван");
    }
    else
    {
        Debug.LogError("GameOverManager.Instance = null!");
        Time.timeScale = 0f;
    }
}
}