using UnityEngine;
using System.Collections.Generic;     

[CreateAssetMenu(menuName = "Artifacts/Artifact", fileName = "New Artifact")]
public class Artifact : ScriptableObject
{
    [Header("Информация")]
    public string artifactName = "Новый Артефакт";
    public Sprite icon;
    public string description = "Описание артефакта";

    [Header("Редкость")]
    public ArtifactRarity rarity = ArtifactRarity.Common;

    [Header("Эффекты")]
    public List<ArtifactEffect> effects = new List<ArtifactEffect>();
}

public enum ArtifactRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

[System.Serializable]
public class ArtifactEffect
{
    public ArtifactStat stat;
    public float value = 10f;
    public bool isPercentage = true;
}

public enum ArtifactStat
{
    MaxHealth,
    Damage,
    AttackSpeed,
    MoveSpeed,
    CritChance
}