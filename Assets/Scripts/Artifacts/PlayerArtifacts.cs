using UnityEngine;
using System.Collections.Generic;

public class PlayerArtifacts : MonoBehaviour
{
    public List<Artifact> ownedArtifacts = new List<Artifact>();

    [Header("Ссылки")]
    public PlayerStats playerStats;   

    public void AddArtifact(Artifact artifact)
    {
        if (artifact == null) return;

        ownedArtifacts.Add(artifact);
        ApplyArtifactEffects(artifact);
        
        Debug.Log($"🎉 Получен артефакт: {artifact.artifactName}");
    }

    private void ApplyArtifactEffects(Artifact artifact)
    {
        if (playerStats == null) return;

        foreach (var effect in artifact.effects)
        {
            playerStats.ModifyStat(effect.stat, effect.value, effect.isPercentage);
        }
    }
}
