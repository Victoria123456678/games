using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Настройки волн")]
    public WaveData[] waves;
    public float timeBetweenWaves = 10f;
    public Transform[] spawnPoints;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool isSpawning = false;

    public event Action<int> OnWaveStart;
    public event Action OnAllWavesComplete;

    public void StartNextWave()
    {
        UIManager.Instance?.UpdateWaveUI(currentWaveIndex + 1);
        if (currentWaveIndex >= waves.Length)
        {
            OnAllWavesComplete?.Invoke();
            Debug.Log("Все волны пройдены!");
            return;
        }

        StartCoroutine(SpawnWaveCoroutine(waves[currentWaveIndex]));
        OnWaveStart?.Invoke(currentWaveIndex + 1);
    }

    private IEnumerator SpawnWaveCoroutine(WaveData wave)
    {
        isSpawning = true;

        foreach (var entry in wave.enemyEntries)
        {
            for (int i = 0; i < entry.count; i++)
            {
                if (entry.enemyPrefab != null)
                {
                    SpawnEnemy(entry.enemyPrefab);
                }
                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }

        isSpawning = false;
    }

    private void SpawnEnemy(GameObject prefab)
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Нет Spawn Points!");
            return;
        }

        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        GameObject enemyObj = PoolManager.Instance.GetObject(prefab, spawnPoint.position, spawnPoint.rotation);

        if (enemyObj.TryGetComponent<EnemyBase>(out var enemy))
        {
            enemy.OnDeath += HandleEnemyDeath;
        }

        enemiesAlive++;
    }

   private void HandleEnemyDeath(EnemyBase enemy)
{
    if (enemy == null) return;

    enemiesAlive--;

  
    enemy.OnDeath -= HandleEnemyDeath;

   
    if (PoolManager.Instance != null)
        PoolManager.Instance.ReturnObject(enemy.gameObject);

    if (enemiesAlive <= 0 && !isSpawning)
    {
        currentWaveIndex++;
        Invoke(nameof(StartNextWave), timeBetweenWaves);
    }
}
}