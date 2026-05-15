    using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;

    [Header("Настройки дропа")]
    [Range(0f, 1f)]
    public float dropChance = 0.4f;

    [Header("Артефакты для дропа")]
    public Artifact[] possibleArtifacts;        

    public ArtifactPickup artifactPickupPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DropReward(Vector3 position)
    {
        if (Random.value > dropChance) return;

        if (possibleArtifacts == null || possibleArtifacts.Length == 0)
        {
            Debug.LogError("[RewardManager] Не назначены possibleArtifacts!");
            return;
        }

        if (artifactPickupPrefab == null)
        {
            Debug.LogError("[RewardManager] artifactPickupPrefab не назначен!");
            return;
        }

      
        Artifact chosenArtifact = possibleArtifacts[Random.Range(0, possibleArtifacts.Length)];

        Vector3 spawnPos = position + Vector3.up * 1.2f;
        GameObject pickupObj = Instantiate(artifactPickupPrefab.gameObject, spawnPos, Quaternion.identity);
        pickupObj.GetComponent<SpriteRenderer>().sprite = chosenArtifact.icon;
        
        ArtifactPickup pickup = pickupObj.GetComponent<ArtifactPickup>();
        if (pickup != null)
        {
            pickup.SetArtifact(chosenArtifact);
            Debug.Log($"[DROPREWARD] Выпал артефакт: {chosenArtifact.artifactName}");
        }
    }
}