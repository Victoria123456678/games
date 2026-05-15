using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Базовые статы")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float damageMultiplier = 1f;
    public float attackSpeedMultiplier = 1f;
    public float moveSpeedMultiplier = 1f;
    public float critChance = 0f;

    private PlayerCombat combat;
    private PlayerHealth health;

    void Start()
    {
        combat = GetComponent<PlayerCombat>();
        health = GetComponent<PlayerHealth>();
        if (combat == null)
        {
            Debug.Log("комбат хуево");

        }
        if (health == null)
        {
            Debug.Log("здоровье хуево");
        }
    }
    public void ModifyStat(ArtifactStat stat, float value, bool isPercentage)
    {
        switch (stat)
        {
            case ArtifactStat.MaxHealth:
                if (isPercentage)
                {
                    maxHealth *= (1 + value / 100f);
                    health.IncreaseHelth((int)value);
                    // health.maxHealth = (int)maxHealth;
                }                  
                else
                {
                    maxHealth += value;
                    health.IncreaseHelth((int)value);
                    // health.maxHealth = (int)maxHealth;
                }
                    
                currentHealth = maxHealth;
                break;

            case ArtifactStat.Damage:
                if (isPercentage)
                {
                    damageMultiplier *= (1 + value / 100f);
                    combat.attackDamage*=damageMultiplier;
                }
                else 
                {
                    damageMultiplier += value / 100f;
                    combat.attackDamage*=damageMultiplier;
                }
                break;

            case ArtifactStat.AttackSpeed:
                if (isPercentage)
                    attackSpeedMultiplier *= (1 + value / 100f);
                else
                    attackSpeedMultiplier += value / 100f;
                break;

            case ArtifactStat.MoveSpeed:
                if (isPercentage)
                    moveSpeedMultiplier *= (1 + value / 100f);
                else
                    moveSpeedMultiplier += value / 100f;
                break;

            case ArtifactStat.CritChance:
                critChance += value;
                break;
        }

        Debug.Log($"Стат изменён: {stat} +{value}{(isPercentage ? "%" : "")}");
    }

    public float GetDamage() => 20f * damageMultiplier; 
}