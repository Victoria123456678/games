using UnityEngine;

[CreateAssetMenu(menuName = "Wave System/Wave Data", fileName = "New Wave")]
public class WaveData : ScriptableObject
{
    public EnemyEntry[] enemyEntries;
    public float spawnInterval = 0.5f;
}

[System.Serializable]
public class EnemyEntry
{
    public GameObject enemyPrefab;
    public int count = 5;
}